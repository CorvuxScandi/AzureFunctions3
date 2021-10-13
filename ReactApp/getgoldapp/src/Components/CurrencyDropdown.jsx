const CurrencyDropDown = ({ setCurrency }) => {
	const currencies = [
		"EUR",
		"AUD",
		"USD",
		"CHF",
		"KYD",
		"GBP",
		"GIP",
		"JOB",
		"OMR",
		"BHD",
		"KWD",
		"SEK",
		"DKK",
		"NOK",
		"JPY",
		"CNY",
		"KRW",
	]
	return (
		<div class="dropdown">
			<button
				class="btn btn-success btn-lg dropdown-toggle"
				type="button"
				id="dropdownMenuButton1"
				data-bs-toggle="dropdown"
				aria-expanded="false">
				Välj Valuta
			</button>
			<ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
				{currencies.map((c) => {
					return (
						<li>
							<button
								class="dropdown-item"
								onClick={() => setCurrency(c)}>
								{c}
							</button>
						</li>
					)
				})}
			</ul>
		</div>
	)
}

export default CurrencyDropDown
