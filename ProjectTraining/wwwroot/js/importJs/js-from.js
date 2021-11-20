$().ready(function() {
	$("#formregister").validate({
		// onfocusout: false,
		// onkeyup: false,
		// onclick: false,
		rules: {
			username: {
				maxlength: 50,
				
			},
			password: {
				
				maxlength: 50,
				minlength: 6,
			},
			
			repassword: {
                equalTo: "#password",
            },
            phone: {
                number : true,
                maxlength: 15,
                minlength: 9,
			},
			address: {
				maxlength: 50,
			}
			
		},
		
	
	});
	
});
