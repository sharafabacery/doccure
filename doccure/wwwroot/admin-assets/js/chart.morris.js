$(function(){
	
	/* Morris Area Chart */
	var AreaData = JSON.parse( document.getElementById('morrisArea').dataset.mydata)
	window.mA = Morris.Area({
		element: 'morrisArea',

	    data: AreaData,
	    xkey: 'Key',
	    ykeys: ['Value'],
	    labels: ['Revenue'],
	    lineColors: ['#1b5a90'],
	    lineWidth: 2,
		
     	fillOpacity: 0.5,
	    gridTextSize: 10,
	    hideHover: 'auto',
	    resize: true,
		redraw: true
	});
	
	/* Morris Line Chart */
	AreaData = JSON.parse(document.getElementById('morrisLine').dataset.mydata)
	window.mL = Morris.Line({
	    element: 'morrisLine',
	    data: 
				AreaData,
	    xkey: 'Key',
	    ykeys: ['Value'],
	    labels: ['users'],
	    lineColors: ['#1b5a90','#ff9d00'],
	    lineWidth: 1,
	    gridTextSize: 10,
	    hideHover: 'auto',
	    resize: true,
		redraw: true
	});
	$(window).on("resize", function(){
		mA.redraw();
		mL.redraw();
	});

});