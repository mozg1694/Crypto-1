﻿using AutoMapper;
using Crypto.Services.Interfaces;
using Crypto.ViewModels.Administrator;
using DBRepository.Interfaces;
using Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypto.Services.Implementation
{
    public class AdministratorService : IAdministratorService
    {
		private readonly IAdministratorRepository _repository; 
		private readonly IMapper _mapper;

		public AdministratorService(IAdministratorRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task DeleteInvestment(int investID)
		{
			await _repository.DeleteInvestment(investID);
		}

		public async Task AddInvestment(InvestmentViewModel request)
		{
			var investment = _mapper.Map<InvestmentViewModel, Investment>(request);
			await _repository.AddInvestment(investment);
		}

		public async Task AddNews(NewsViewModel request)
		{
			var news = _mapper.Map<NewsViewModel, News>(request);
			await _repository.AddNews(news);
		}

        public async Task<RateDETViewModel> UpdateDETRate(RateDETViewModel request)
        {
			var rate = _mapper.Map<RateDETViewModel, Balance>(request);
			var newRate = await _repository.UpdateDETRate(rate);
			return _mapper.Map<Balance, RateDETViewModel>(newRate);
        }

		public async void UpdateBTCRate()
		{
			var uri = "https://blockchain.info/tobtc?currency=USD&value=1";

			HttpClient client = new HttpClient();
			string data = await client.GetStringAsync(uri);

			data = data.Replace(".", ",");
			var rate = _mapper.Map<string, Balance>(data);
			await _repository.UpdateBTCRate(rate);
		}

		public async Task<List<RefUserViewModel>> GetRef(int Ref)
		{
			var response = await _repository.GetRef(Ref);
			return _mapper.Map<List<User>, List<RefUserViewModel>>(response);
		}
        #region Dev
        public async Task<List<UserViewModel>> GetUsers()
		{
			var response = await _repository.GetUsers();
			return _mapper.Map<List<object>, List<UserViewModel>>(response);
		}
		public async Task DelUser(int Id)
		{
			await _repository.DelUser(Id);
		}
        #endregion

    }
}
