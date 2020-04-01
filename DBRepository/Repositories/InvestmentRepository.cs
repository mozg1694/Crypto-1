﻿using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Models;
using System.Linq;

namespace DBRepository.Repositories
{
	public class InvestmentRepository : BaseRepository, IInvestmentRepository
	{
		public InvestmentRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

		public async Task<Investment> GetInvestment(string name)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
				return await context.Investments.FirstOrDefaultAsync(i => i.Name == name);
		}

		public async Task<Page<Investment>> GetPosts(int index, int pageSize, string investment = null)
		{
			var result = new Page<Investment>() { CurrentPage = index, PageSize = pageSize };

			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				var query = context.Investments.AsQueryable();
				if (!string.IsNullOrWhiteSpace(investment))
				{
					query = query.Where(i => i.Name == investment);
				}

				result.TotalPages = await query.CountAsync();
				result.Records = await query.OrderByDescending(i => i.CreatedDate).Skip(index * pageSize).Take(pageSize).ToListAsync();
			}

			return result;
		}

		public async Task AddInvestment(Investment investment)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				context.Investments.Add(investment);
				await context.SaveChangesAsync();
			}
		}

		public async Task DeleteInvestment(int investID)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				var invest = new Investment() { InvestmentID = investID };
				context.Investments.Remove(invest);
				await context.SaveChangesAsync();
			}
		}
	}
}
