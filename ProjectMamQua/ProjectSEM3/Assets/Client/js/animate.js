

jQuery(document).ready(function($) {



    function elementsAnimate() {
        var windowWidth = window.innerWidth || document.documentElement.clientWidth;
        var animate = $('.animate');
        var animateDelay = $('.animate-delay-outer');
        if (windowWidth > 767) {
            animate.bind('inview', function (event, visible) {
                if (visible && !$(this).hasClass("animated")) {
                    $(this).addClass("animated");
                }
            });
			animateDelay.bind('inview', function (event, visible) {
                if (visible && !$(this).hasClass("animated")) {
                    var j = -1;
                    var $this = $(this).find(".animate-delay");
                    $this.each(function () {
                        var $this = jQuery(this);
                        j++;
                        setTimeout(function () {
                            $this.addClass("animated");
                        }, 200 * j);
                    });
					$(this).addClass("animated");
                }
            });
        }
    }



	// !- Animation "onScroll" loop
//	function doAnimation() {
//			var j = -1;
//			$(".animate-element:not(.start-animation):in-viewport").each(function () {
//				var $this = $(this);
//	
//				if (!$this.hasClass("start-animation") && !$this.hasClass("animation-triggered")) {
//					$this.addClass("animation-triggered");
//					j++;
//					setTimeout(function () {
//						$this.addClass("start-animation");
//						if($this.hasClass("skills")){
//							$this.animateSkills();
//						};
//					}, 200 * j);
//				};
//			});
//	};
//

	// !- Fire animation
	elementsAnimate();
		$(window).on("scroll", function () {
			elementsAnimate();
		});


});