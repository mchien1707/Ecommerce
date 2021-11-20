$(document).ready(function() {
    
	$('#username').on('keypress', function (e) {
		var blockSpecialRegex = /[~|*`!@#$%^&()_={}[\]:;,.<>+\/?-]/;
		  var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		  if(blockSpecialRegex.test(key) || $.isNumeric(key)){
			e.preventDefault();
			return "sai";
		}
	});
	
	$('#email').on('keypress', function (e) {
		var blockSpecialRegex = /[~|*`!#$%^&()_={}[\]:;,<>+\/?-]/;
		  var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		  if(blockSpecialRegex.test(key)){
			e.preventDefault();
			return "sai";
		}
    });	
});