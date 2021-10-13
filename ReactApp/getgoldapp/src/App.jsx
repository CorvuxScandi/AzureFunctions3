import { useEffect, useState } from "react"
import "./App.css"
import "../node_modules/bootstrap/dist/css/bootstrap.css"
import "../node_modules/bootstrap/dist/js/bootstrap.js"
import PricePresenter from "./Components/PricePresenter"
import CurrencyDropDown from "./Components/CurrencyDropdown"
function App() {
	const [goldData, setGoldData] = useState({})
	const [currency, setCurrency] = useState("EUR")

	useEffect(() => {
		fetch("http://localhost:7071/api/MyGoldApi?currency=" + currency)
			.then((res) => {
				return res.json()
			})
			.then((data) => {
				console.log(data)
				setGoldData(data)
			})
	}, [currency])
	const getData = () => {
		let url =
			"http://localhost:7071/api/MyGoldApi?currency=" + currency
		const request = new XMLHttpRequest()
		request.open("GET", url)
		request.send()
	}
	window.onload = getData
	return (
		<div className="container-fluid bg-dark text-light">
			<div className="row">
				<div className="col"></div>
				<div className="col-auto my-5">
					<div className="row">
						<div className="col">
							<PricePresenter data={goldData} />
						</div>
					</div>
					<div className="row mt-3">
						<div className="col">
							<button
								className="btn btn-danger btn-lg"
								onClick={getData}>
								Uppdatera
							</button>
						</div>
						<div className="col">
							<CurrencyDropDown setCurrency={setCurrency} />
						</div>
					</div>
				</div>
				<div className="col"></div>
			</div>
		</div>
	)
}

export default App
