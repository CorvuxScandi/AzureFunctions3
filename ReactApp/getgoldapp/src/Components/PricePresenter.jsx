const PricePresenter = ({ data }) => {
	return (
		<div className="border border-warning p-3">
			<h1>Current XAU Price</h1>
			<h2>
				{data.Currency}: {data.CurrentPrice}
			</h2>
		</div>
	)
}

export default PricePresenter
