/* 	------------------------- 
        
        Inicia todos los plugins

------------------------- */

$(document).ready(function () {
	
	// Popup de imágenes
	$(function() {
				
		var foo = $('#prop-dest');
		foo.poptrox({
		usePopupCaption: true
		});
			
	});
	
	// DATEPICKERS 
	// http://eternicode.github.io/bootstrap-datepicker
	// http://vitalets.github.io/bootstrap-datepicker/
	
	;(function($){
	$.fn.datepicker.dates['es'] = {
		days: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"],
		daysShort: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom"],
		daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do"],
		months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
		monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
		today: "Hoy"
	};
	}(jQuery));

	
	
	$('#checkin-datepicker input, #checkout-datepicker input').datepicker({
    	format: "dd/mm/yyyy",
		minViewMode: 1,
		todayBtn: true,
		language: "es"
	});
	
	$('#dp6').datepicker({
		language: "es",
		format: "dd/mm/yyyy",
		startDate: "01/06/2015",
		endDate: "30/06/2015"
	});
	$('#dp7').datepicker({
		language: "es",
		format: "dd/mm/yyyy",
		startDate: "01/07/2015",
		endDate: "30/07/2015"
	});
	
	// COUNTERS (http://www.virtuosoft.eu/code/bootstrap-touchspin/)
	$("input[name='ambientes']").TouchSpin({
      verticalbuttons: true,
	  min: 1,
      max: 4,
    });
	
	$("input[name='huespedes']").TouchSpin({
      verticalbuttons: true,
	  min: 1,
      max: 10,
    });
	

});