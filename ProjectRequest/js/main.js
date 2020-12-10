jQuery(document).ready(function($){
	//open popup TW
	$('#tw').on('click', function(event){
		event.preventDefault();
		$('#tw-popup').addClass('is-visible');
	});
	
	//close popup
	$('#tw-popup').on('click', function(event){
		if( $(event.target).is('#tw-close') || $(event.target).is('#tw-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#tw-popup').removeClass('is-visible');
	    }
    });
	//open popup AC
	$('#ac').on('click', function(event){
		event.preventDefault();
		$('#ac-popup').addClass('is-visible');
	});
	
	//close popup
	$('#ac-popup').on('click', function(event){
		if( $(event.target).is('#ac-popup-close') || $(event.target).is('#ac-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#ac-popup').removeClass('is-visible');
	    }
    });
	
	//open popup EC
	$('#ec').on('click', function(event){
		event.preventDefault();
		$('#ec-popup').addClass('is-visible');
	});
	
	//close popup
	$('#ec-popup').on('click', function(event){
		if( $(event.target).is('#ec-popup-close') || $(event.target).is('#ec-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#ec-popup').removeClass('is-visible');
	    }
    });

	//open popup CC
	$('#cc').on('click', function(event){
		event.preventDefault();
		$('#cc-popup').addClass('is-visible');
	});
	
	//close popup
	$('#cc-popup').on('click', function(event){
		if( $(event.target).is('#ec-popup-close') || $(event.target).is('#cc-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#cc-popup').removeClass('is-visible');
	    }
    });	
	
	
	//open popup MJ
	$('#mj').on('click', function(event){
		event.preventDefault();
		$('#mj-popup').addClass('is-visible');
	});
	
	//close popup
	$('#mj-popup').on('click', function(event){
		if( $(event.target).is('#mj-popup-close') || $(event.target).is('#mj-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#mj-popup').removeClass('is-visible');
	    }
    });		
	
	
		//open popup EK
	$('#ek').on('click', function(event){
		event.preventDefault();
		$('#ek-popup').addClass('is-visible');
	});
	
	//close popup
	$('#ek-popup').on('click', function(event){
		if( $(event.target).is('#ek-popup-close') || $(event.target).is('#ek-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#ek-popup').removeClass('is-visible');
	    }
    });	
	
			//open popup AM
	$('#am').on('click', function(event){
		event.preventDefault();
		$('#am-popup').addClass('is-visible');
	});
	
	//close popup
	$('#am-popup').on('click', function(event){
		if( $(event.target).is('#am-popup-close') || $(event.target).is('#am-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#am-popup').removeClass('is-visible');
	    }
    });	
	
	
			//open popup PM
	$('#pm').on('click', function(event){
		event.preventDefault();
		$('#pm-popup').addClass('is-visible');
	});
	
	//close popup
	$('#pm-popup').on('click', function(event){
		if( $(event.target).is('#pm-popup-close') || $(event.target).is('#pm-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#pm-popup').removeClass('is-visible');
	    }
    });	
	
	
			//open popup CN
	$('#cn').on('click', function(event){
		event.preventDefault();
		$('#cn-popup').addClass('is-visible');
	});
	
	//close popup
	$('#cn-popup').on('click', function(event){
		if( $(event.target).is('#cn-popup-close') || $(event.target).is('#cn-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#cn-popup').removeClass('is-visible');
	    }
    });	
	
	
		//open popup DP
	$('#dp').on('click', function(event){
		event.preventDefault();
		$('#dp-popup').addClass('is-visible');
	});
	
	//close popup
	$('#dp-popup').on('click', function(event){
		if( $(event.target).is('#dp-popup-close') || $(event.target).is('#dp-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#dp-popup').removeClass('is-visible');
	    }
    });	
	
	
			//open popup IS
	$('#is').on('click', function(event){
		event.preventDefault();
		$('#is-popup').addClass('is-visible');
	});
	
	//close popup
	$('#is-popup').on('click', function(event){
		if( $(event.target).is('#is-popup-close') || $(event.target).is('#is-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#is-popup').removeClass('is-visible');
	    }
    });
	
			//open popup MW
	$('#mw').on('click', function(event){
		event.preventDefault();
		$('#mw-popup').addClass('is-visible');
	});
	
	//close popup
	$('#mw-popup').on('click', function(event){
		if( $(event.target).is('#mw-popup-close') || $(event.target).is('#mw-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#mw-popup').removeClass('is-visible');
	    }
    });
	
		//open popup Graphic Intern
	$('#gi').on('click', function(event){
		event.preventDefault();
		$('#gi-popup').addClass('is-visible');
	});
	
	//close popup
	$('#gi-popup').on('click', function(event){
		if( $(event.target).is('#gi-popup-close') || $(event.target).is('#gi-popup') ) {
			event.preventDefault();
			$(this).removeClass('is-visible');
		}
	});
	//close popup when clicking the esc keyboard button
	$(document).keyup(function(event){
    	if(event.which=='27'){
    		$('#gi-popup').removeClass('is-visible');
	    }
    });
	
});