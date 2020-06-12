import React, { Component } from 'react'
import DashHeader from './components/header'
import Sidebar from './components/sidebar'
import DashContent from './components/dashcontent/content'
import '../styles/dashboard.scss'
import { SvgSprite }  from './svg/icons'

class Dashboard extends Component {
	
	state = {
		isClosed: false
	};

	toggle = (value) => {
		this.setState({ isClosed: !value })
	}
		
	render() {
		return (
			<div className="dash">
				<SvgSprite />
				<div className={this.state.isClosed ? "dash-wrapper-closed" : "dash-wrapper"}>
					<DashHeader />
					<Sidebar isClosed={this.state.isClosed} toggle={this.toggle} />
					<DashContent />
				</div>
			</div>
		)
	}
}
/*
const mapStateToProps = state => {
	console.log(state)
	return {
		Sidebar: state.Sidebar
	}
}

const mapDispatchToProps = (dispatch) => ({
		toggleSidebar: (isClosed) => dispatch(toggleSidebar(), isClosed),
})
*/
export default Dashboard
