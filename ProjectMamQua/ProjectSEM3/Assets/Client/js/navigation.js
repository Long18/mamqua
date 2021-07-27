(function ($) {

    "use strict";
	
	$(window).load(function () {
        var $dropdowns = $('li.dropdown'); // Specifying the element is faster for older browsers

        var deviceAgent = navigator.userAgent.toLowerCase();
        var agentID = deviceAgent.match(/(iphone|ipod|ipad)/);
        if (agentID) {

            // mobile code here 
            /**
             * Touch events
             *
             * @description Support click to open if we're dealing with a touchscreen
             */
            if ('ontouchstart' in document.documentElement) {
                $dropdowns.each(function () {
                    var $this = $(this);

                    this.addEventListener('touchstart', function (e) {
                        if (e.touches.length === 1) {
                            // Prevent touch events within dropdown bubbling down to document
                            e.stopPropagation();

                            // Toggle hover
                            if (!$this.hasClass('hover')) {
                                // Prevent link on first touch
                                if (e.target === this || e.target.parentNode === this) {
                                    e.preventDefault();
                                }

                                // Hide other open dropdowns
                                $dropdowns.removeClass('hover');
                                $this.addClass('hover');
								$('#nav').addClass('focus');
                                // Hide dropdown on touch outside
                                document.addEventListener('touchstart', closeDropdown = function (e) {
                                    e.stopPropagation();
                                    $dropdowns.removeClass('hover');
                                    $this.removeClass('hover');
									$('#nav').removeClass('focus');
                                    document.removeEventListener('touchstart', closeDropdown);
                                });
                            }
                        }
                    }, false);
                });
            }


        } else {
            /**
             * Mouse events
             *
             * @description Mimic hoverIntent plugin by waiting for the mouse to 'settle' within the target before triggering
             */
            $dropdowns.on('mouseover', function () // Mouseenter (used with .hover()) does not trigger when user enters from outside document window
                {
                    var $this = $(this);

                    if ($this.prop('hoverTimeout')) {
                        $this.prop('hoverTimeout', clearTimeout($this.prop('hoverTimeout')));
                    }

                    $this.prop('hoverIntent', setTimeout(function () {
                        $this.addClass('hover');
                    }));
                })
                .on('mouseleave', function () {
                    var $this = $(this);

                    if ($this.prop('hoverIntent')) {
                        $this.prop('hoverIntent', clearTimeout($this.prop('hoverIntent')));
                    }

                    $this.prop('hoverTimeout', setTimeout(function () {
                        $this.removeClass('hover');
                    }));
                });

        }

}); //]]>

})($);