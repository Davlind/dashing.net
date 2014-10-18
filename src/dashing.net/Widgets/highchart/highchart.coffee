class Dashing.Highchart extends Dashing.Widget

	createChart: (data) ->
	
		container = $(@node).find('.highchart-container')
	
		if $(container)[0]
			
			options =
				chart:
					renderTo: $(container)[0]
					backgroundColor: data.backgroundColor
					marginTop: 2
					type: data.type
					
				title:
					text: ' '
				
				yAxis:
					title:
						enabled: false
						
				xAxis:
					categories: data.categories
	
				legend:
					enabled: false
				
				series: data.series
				
				plotOptions: data.plotOptions
				
			if 'legend' of data
				options.legend = data.legend
			
			@chart = new Highcharts.Chart(options)

	onData: (data) ->
		@createChart(data)
