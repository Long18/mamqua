(function ($) {

    "use strict";
	function ProductImgHeight() {
		var imgHeight = 0;
		$.each($('.product .product-image-wrapper'), function(){
					imgHeight = $(this).find('img').height();
					$(this).height(imgHeight);
					$(this).parent('.product').next('.preview').find('.wrapper .big_image').height(imgHeight);
	});}

	function calculateProductsInRow(row) {
		jQuery('.product-row-divider').each(function () {
			 $(this).remove();  
		});
		var productsInRow = 0;
		jQuery(row).children('.product').each(function () {
			if (jQuery(this).prevAll(':eq(1)').length > 0) {
				if (jQuery(this).position().top != jQuery(this).prevAll(':eq(1)').position().top) return false;
				productsInRow++;
			}
			else {
				productsInRow++;
			}
		});
		//console.log (productsInRow);
		if (jQuery(row).children('.preview').length){
			jQuery(row).children(':nth-child('+2*productsInRow+'n)').after('<div class="product-row-divider clearfix"></div>');}
		else {
			jQuery(row).children(':nth-child('+productsInRow+'n)').after('<div class="product-row-divider clearfix"></div>');
		}
	}

    function PreviewInit() {

        var product = $('.product');
        var preview = $('.preview');

        product.find('.product-image-wrapper.onhover').bind('mouseenter', function () {
            var pos = $(this).parent().position();
            var width = $(this).outerWidth();
            var width1 = $(this).parent().next(preview).outerWidth();
            $(this).parent().addClass('hover');
            var width2 = width1 - width;
            $(this).parent().next(preview).css({
                top: pos.top + 10 + "px",
                left: (pos.left - width2 + 30) + "px"
            });
            $(this).parent().next(preview).find("small").css({
                top: pos.top + 10 + "px",
                left: (pos.left - width2 + 30) + "px"
            });

            preview.hide();
            $(this).parent().next(preview).show();
        });

        preview.bind('mouseleave', function () {
            product.removeClass('hover');
            $(this).stop().hide();
            $('.big_image a img', this).attr('src', $('.big_image a img', this).attr("data-rel"));
        });

        preview.find("a.image").bind('mouseenter', function () {
            $(this).parent().next().find('.big_image a img').stop(true, true).animate({
                opacity: 0
            }, 200);
            var image = $(this).attr("data-rel");
            $(this).parent().next().find('.big_image a img').attr('src', image);
            $(this).parent().next().find('.big_image a img').stop(true, true).animate({
                opacity: 1
            }, 800);
            return false;
        });
    }

    function TopSlider(flexSliderTop) {
        var container = $(".container");
        var w0 = $(document).width();
        var w1 = (w0 - container.width()) * 0.5 - 0;

        flexSliderTop.find(".flex-direction-nav .flex-next").css({
            "right": w1 + "px"
        });
        flexSliderTop.find(".flex-direction-nav .flex-prev").css({
            "left": w1 + "px"
        });
        flexSliderTop.find(".next-slider").css({
            "right": w1 + "px"
        });
        flexSliderTop.find(".prev-slider").css({
            "left": w1 + "px"
        });

    };

    function elementsAnimate() {
        var windowWidth = window.innerWidth || document.documentElement.clientWidth;
        var animate = $('.animate');
        var animateDelay = $('.animate-delay-outer');
        var animateDelayItem = $('.animate-delay');
        if (windowWidth > 767 && !isiPhone()) {
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
                }spy
            });
        } else {
            animate.each(function () {
                $(this).removeClass('animate');
            });
            animateDelayItem.each(function () {
                $(this).removeClass('animate-delay');
            });
        }
    }

    function isTouchDevice() {
        return (typeof (window.ontouchstart) != 'undefined') ? true : false;
    }

    function isiPhone() {
        return (
            (navigator.userAgent.toLowerCase().indexOf("iphone") > -1) ||
            (navigator.userAgent.toLowerCase().indexOf("ipod") > -1)
        );
    }
    var resize_picholder;

    function resizePicHolder() {
        var windowWidth = window.innerWidth || document.documentElement.clientWidth;
        var ppPicHolder = $('.pp_pic_holder');
        var left_pic_holder = windowWidth - ppPicHolder.width();
        ppPicHolder.animate({
            left: left_pic_holder / 2
        });
    }

    $(document).ready(function () {


        var product = $('.product');
        var preview = $('.preview');
        var windowWidth = window.innerWidth || document.documentElement.clientWidth;
        var pageBody = $('body');
        var priceSlider = $("#noUiSlider");
        var cloudZoom = $('#CloudZoom');
        var cloudGallery = $('.cloudzoom-gallery');
        var cloudGalleryOuter = $('.product-more-views ul');
        var listingSort = $('.sort-by');
        var isotopeOuter = $('.isotope-outer');
        var selectCustom = $("select.custom");
        var flexSliderBanners = $('.flexslider.banners');
        var flexSliderTop = $(".flexslider.big");
        var flexContentCarousel = $('.flexslider.carousel-content');
		var flexTestimonials = $('.carousel-testimonials .flexslider');
        var flexSliderSmall = $('.flexslider.small');
        var imageCloudZoom = $('.product-image img.cloudzoom');
        var carousel = $('.carousel');
        var hoverfold = $('#hoverfold');
        var accordion = $('.accordion');
        var contentTab = $('.contentTab');
        var tableStripped = $("table.striped");
        var navigation = $("nav");
        var footer = $("#footer");
        var footerPopup = $("#footer_popup");
        var rightToolbar = $("#right_toolbar");
        var productVerticalCarousel = $('.flexslider.vertical');
        var topLink = $('#topline .fadelink, .header_v_2 .fadelink');
        var headerShop = $("#header .shoppingcart .fadelink");
        var rightShop = $("#right_toolbar .shoppingcart");
        var rightSearch = $("#right_toolbar .form-search");
        var arrowPrev = $(".es-nav-prev");
        var arrowNext = $(".es-nav-next");
        var arrowPrev1 = $(".direction-nav a.prev");
        var arrowNext1 = $(".direction-nav a.next");
        var arrowPrev2 = $(".flexslider.vertical .flex-prev");
        var arrowNext2 = $(".flexslider.vertical .flex-next");
        var arrowPrev3 = $('.carousel_prev');
        var arrowNext3 = $('.carousel_next');
        var progressBar = $('.progress .bar');
        var smallPreview = $(".small_preview");
        var shoppingCartMini = $(".shopping_cart_mini");
        var ppPicHolder = $('.pp_pic_holder');
        var backTop = $('#back-top a');
        var brands = $('#brands0 ul');
        var brandsImg = $(".brands_block a img");

        var productsRow = $(".big_with_description");
		productsRow.each(function () {
			calculateProductsInRow(this);
		});

        if (isTouchDevice()) {
			$( '#megamenu li:has(ul)' ).doubleTapToGo();
            var mobileHover = function () {
                $('*').on('touchstart', function () {
                    $(this).trigger('hover');
                }).on('touchend', function () {
                    $(this).trigger('hover');
                });
            };
            mobileHover();
            pageBody.addClass('touch')
            footer.click(function () {
                if (!jQuery(this).find("i.icon-up").hasClass("icon-down")) {
                    if (footerPopup.hasClass("allowHover") && footerPopup.css('position') == 'absolute') {
                        footerPopup.stop(true, false).slideDown(300);
                        jQuery(this).find("i.icon-up").addClass("icon-down");
                    }
                } else {
                    if (footerPopup.hasClass("allowHover") && footerPopup.css('position') == 'absolute') {
                        footerPopup.stop(true, false).slideUp(300);
                        jQuery(this).find("i.icon-up").removeClass("icon-down");
                    }
                }
            });
            topLink.click(function () {
                $(".ul_wrapper").each(function () {
                    jQuery(this).fadeOut(0)
                })
                if (!$(this).hasClass('open')) {
                    topLink.each(function () {
                        $(this).removeClass('open');
                    })
                    $(this).find(".ul_wrapper").fadeIn(200, "linear");
                    $(this).addClass('open');
                } else {
                    $(this).find(".ul_wrapper").fadeOut(200, "linear");
                    $(this).removeClass('open');
                }
            });
        } else {
            pageBody.addClass('notouch');
            $('div.noHover').hover(function () {
                footerPopup.toggleClass("allowHover");
            });
            footer.hover(function () {
                if (footerPopup.hasClass("allowHover") && footerPopup.css('position') == 'absolute') {
                    footerPopup.stop(true, false).slideDown(300);
                    jQuery(this).find("i.icon-up").addClass("icon-down");
                }
            }, function () {
                if (footerPopup.hasClass("allowHover") && footerPopup.css('position') == 'absolute') {
                    footerPopup.stop(true, false).slideUp(100);
                    $(this).find("i.icon-up").removeClass("icon-down");
                }
            });
            topLink.hover(function () {
                $(this).find(".ul_wrapper").stop(true).fadeToggle(200, "linear");
            });
			if ($(".parallax").length > 0) $(".parallax").parallax({
				speed: 0,
				axis: "y"
    		});
        }

        if (isiPhone()) {
            pageBody.addClass('iphone')
        };




        /*	DETECT IF IE */

        var trident = !! navigator.userAgent.match(/Trident\/7.0/);
        var net = !! navigator.userAgent.match(/.NET4.0E/);
        var IE11 = trident && net
        var IEold = (navigator.userAgent.match(/MSIE/i) ? true : false);
        if (IE11 || IEold) {
            jQuery('body').addClass('msie');
        } else {
            jQuery('body').addClass('no_msie')
        }

		var themeDemos = $("#themeDemos");
		var demoView = $('.demoView');
		var demoViews = $('.demoViews');

		demoViews.css({height: themeDemos.height() - 53});
		themeDemos.css({marginBottom: -themeDemos.height() + 50});
		themeDemos.find ('a').click(function() {
			if (themeDemos.hasClass('open-panel')){themeDemos.removeClass('open-panel'); themeDemos.animate({marginBottom: -themeDemos.height() + 50})}
			else {themeDemos.addClass('open-panel'); themeDemos.animate({marginBottom: 0});}		  
		});
        themeDemos.find('.item:not(".comingsoon")').mouseenter(function () {
          demoView.stop(true, true).show(0);
		  $('.demoView img').attr('src', $(this).attr("data-img"));
 		  $('.demoView .title').html($(this).attr("data-title"));
       });

        themeDemos.find('.item:not(".comingsoon")').mouseleave(function () {
          demoView.stop(true, true).hide(0)
        });

		$(window).resize(function () {
			demoViews.css({height: themeDemos.height() - 53});
			if (!themeDemos.hasClass('open-panel')){themeDemos.animate({marginBottom: -themeDemos.height() + 50})}
			else {themeDemos.animate({marginBottom: 0});}		  
		})

        PreviewInit();
        rightToolbar.hide();
		
		// product listing view as
		var viewGrid = $(".view-grid"),
        	viewList = $(".view-list"),
        productList = $(".product-listing");
		viewGrid.click(function (e) {
			viewList.removeClass('active');
			viewGrid.addClass('active');
			productList.removeClass("product-list").addClass("product-grid");
			if (isotopeOuter.length != 0) {
                isotopeOuter.isotope('reLayout');
            }
			e.preventDefault()
		});
		viewList.click(function (e) {
			viewGrid.removeClass('active');
			viewList.addClass('active');
			productList.removeClass("product-grid").addClass("product-list");
			if (isotopeOuter.length != 0) {
                isotopeOuter.isotope('reLayout');
            }
			e.preventDefault()
		})

        // begin collapsed menu
        $('label.tree-toggler').each(function () {
            if ($(this).parent().find('ul.nav-list').length != 0) {
                $(this).append("<span class='collapse_button'>+</span>");
            }
        });
        $('label.tree-toggler span.collapse_button').click(function () {
            if (!$(this).parent().parent().hasClass('active')) {
                $(this).html('&#8211;');
                $(this).addClass('collapsed');
                $(this).parent().parent().children('ul.nav-list').show(300);
                $(this).parent().parent().addClass('active');
            } else {
                $(this).html('+');
                $(this).removeClass('collapsed');
                $(this).parent().parent().find('ul.nav-list').hide(300);
                $(this).parent().parent().removeClass('active');
                $(this).parent().parent().find('li').removeClass('active');
                $(this).parent().parent().find('span.collapse_button').html('+');
            }
        });
        //  end collapsed menu

        // price slider

        priceSlider.empty().noUiSlider('init', {
            scale: [0, 2000],
            start: [0, 800],
            step: false,
            change: function () {
                var values = $(this).noUiSlider('value');
                $(this).find('.noUi-lowerHandle .infoBox').text('$' + values[0]);
                $(this).find('.noUi-upperHandle .infoBox').text('$' + values[1]);

            }
        }).find('.noUi-handle div').each(function (index) {

            $(this).append('<span class="infoBox">$' + $(this).parent().parent().noUiSlider('value')[index] + '</span>');

        });


        if (cloudZoom.length != 0) {
            cloudZoom.CloudZoom({
                zoomMatchSize: true
            });
            cloudGallery.CloudZoom();
        }


        listingSort.change(function () {
            //console.log("Selected Option:" + $(this).val());
            product.removeClass('hover');
            preview.hide();
            isotopeOuter.isotope({
                sortBy: $(this).val()
            });
        });

        selectCustom.each(function () {
            $(this).selectbox();
        })

        // change accordion caret when collapsing
        accordion.find('.accordion-toggle').click(function () {
            if ($(this).hasClass('collapsed')) {
                accordion.find('.accordion-toggle').not(this).addClass('collapsed');
            }
        })
		
         // products carousel
        carousel.each(function() {
            $(this).elastislide({
                speed: 600
            });
			if ($(this).find('.product_outer').width() <= $(this).find('.row').width()+10) {
                $(this).find('.es-nav').hide();

            }
        })
		

        // tabs	
        contentTab.find('a').click(function (e) {
            e.preventDefault();
            jQuery(this).tab('show');
        })
		
		jQuery('a[data-toggle="tab"]').click(function (e) {
			e.preventDefault();
			var href = jQuery.attr(this, 'href'),
				root = jQuery('html, body'),
				topMargin = 200;
			jQuery('a[href="'+href+'"]').tab('show');
			root.animate({
				scrollTop: $(href).offset().top - topMargin
			}, 200, function () {
				window.location.hash = href;
			});
		})   


	
        jQuery("#carousel_tabs>a").click(function(e) {
			jQuery("#carousel_tabs>a").removeClass("active");
			jQuery(this).addClass("active");
			jQuery("#carousel_tabs_content .carousel").hide();
			var t_content = jQuery(this).attr("href");
			jQuery(t_content).show();
			e.preventDefault();
    	})
		
        jQuery("#carousel_tabs>a:first").trigger("click");
		
        // stripped table
        tableStripped.find("tr:odd").addClass("odd");

        // collapse top navigation menu
        navigation.find(".collapse").collapse();


        headerShop.mouseenter(function () {
            jQuery(this).parent().find(".shopping_cart_mini").stop(true, true).fadeIn(200, "linear");
        });

        headerShop.mouseleave(function () {
            jQuery(this).parent().find(".shopping_cart_mini").stop(true, true).fadeOut(200, "linear");
        });

        rightShop.mouseenter(function () {
            jQuery(this).find(".shopping_cart_mini").stop(true, true).fadeIn(200, "linear");
        });

        rightShop.mouseleave(function () {
            jQuery(this).find(".shopping_cart_mini").stop(true, true).fadeOut(200, "linear");
        });


        rightSearch.mouseenter(function () {
            $(this).find('input').animate({
                right: 48,
                width: 275
            }, 300);
        });
        rightSearch.mouseleave(function () {
            $(this).find('input').stop(true, false).animate({
                right: 20,
                width: 0
            }, 300);
        });




        //  content carousel - our services page
        flexContentCarousel.flexslider({
            animation: "slide",
            pauseOnHover: true,
            controlNav: false,
            animationSpeed: 300,
            prevText: "<i class='prev icon-left-thin'></i>",
            nextText: "<i class='next icon-right-thin'></i>"

        });
		
		//  content carousel - Testimonials
        flexTestimonials.flexslider({
            animation: "slide",
            pauseOnHover: true,
            controlNav: false,
            animationSpeed: 300,
            prevText: "<i class='prev icon-left-thin'></i>",
            nextText: "<i class='next icon-right-thin'></i>"

        });

        //  banner slider on side column

        flexSliderBanners.flexslider({
            animation: "slide",
            pauseOnHover: true,
            controlNav: false,
            prevText: "<i class='icon-left-thin'></i>",
            nextText: "<i class='icon-right-thin'></i>"

        });

        // related product vertical carousel
        productVerticalCarousel.flexslider({
            animation: "slide",
            autoplay: false,
            minItems: 2,
            direction: "vertical",
            pauseOnHover: true,
            controlNav: false,
            prevText: "<i class='icon-down'></i>",
            nextText: "<i class='icon-up'></i>"

        });

        // top slider  on listing page
        flexSliderSmall.flexslider({
            animation: "slide",
            pauseOnHover: true,
            controlNav: false,
            prevText: "<i class='icon-left-thin'></i>",
            nextText: "<i class='icon-right-thin'></i>"

        });
        // big slider  on home page

        if (flexSliderTop.length != 0) {
            flexSliderTop.flexslider({
                animation: "slide",
                controlNav: false,
                prevText: "<i class='icon-left-thin'></i>",
                nextText: "<i class='icon-right-thin'></i>",
                start: function (slider) {
                    flexSliderTop.find('li > a > img').animate({
                        'opacity': 1
                    });
                    elementsAnimate();
                }
            });
        } else {
            elementsAnimate();
        }
		
		var carousel_tabs = $("#carousel_tabs>a");
		
		carousel_tabs.click(function () {
			carousel_tabs.removeClass("active");
			$(this).addClass("active");
			$("#carousel_tabs_content .carousel").hide();
			var t_content = jQuery(this).attr("href");
			$(t_content).show();
			ProductImgHeight();
			return false;
		})
		$("#carousel_tabs>a:first").trigger("click");


        TopSlider(flexSliderTop);


        //3D gallery

        if (hoverfold.length != 0) {

            //start the hoverfold plugin

            Modernizr.load({
                test: Modernizr.csstransforms3d && Modernizr.csstransitions,
                yep: ['js/jquery.hoverfold.js'],
                nope: 'css/fallback.css',
                callback: function (url, result, key) {

                    if (url === 'js/jquery.hoverfold.js') {
                        $('#hoverfold div').hoverfold();
                    }

                }
            });

            var $container = hoverfold;

            $container.isotope({
                itemSelector: '.span4'
            });


            var $optionSets = jQuery('#options .option-set'),
                $optionLinks = $optionSets.find('a');

            $optionLinks.click(function () {
                var $this = $(this);
                // don't proceed if already selected
                if ($this.hasClass('selected')) {
                    return false;
                }
                var $optionSet = $this.parents('.option-set');
                $optionSet.find('.selected').removeClass('selected');
                $this.addClass('selected');

                // make option object dynamically, i.e. { filter: '.my-filter-class' }
                var options = {},
                    key = $optionSet.attr('data-option-key'),
                    value = $this.attr('data-option-value');
                // parse 'false' as false boolean
                value = value === 'false' ? false : value;
                options[key] = value;
                if (key === 'layoutMode' && typeof changeLayoutMode === 'function') {
                    // changes in layout modes need extra logic
                    changeLayoutMode($this, options)
                } else {
                    // otherwise, apply new options
                    $container.isotope(options);
                }

                return false;
            });
        }



        $('#back-top a').hover(function () {
            $(this).stop().animate({
                "opacity": 0.6
            });
        }, function () {
            jQuery(this).stop().animate({
                "opacity": 1
            });
        });

        $('#back-top a').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 400);
            return false;
        });

        // small previews on hover

        arrowPrev.hover(function () {
            if (!$(this).hasClass("disable")) {
                $(this).parent().parent().find(".small_preview.prev").stop(true, true).fadeToggle(400, "linear");
            }
        });

        arrowNext.hover(function () {
            if (!$(this).hasClass("disable")) {
                $(this).parent().parent().find(".small_preview.next").stop(true, true).fadeToggle(400, "linear");
            }
        });
        arrowPrev.mouseleave(function () {
            $(".small_preview.prev").stop(true, true).fadeOut(100, "linear");
        });

        arrowNext.mouseleave(function () {
            $(".small_preview.next").stop(true, true).fadeOut(100, "linear");
        });


        arrowPrev1.hover(function () {
            $(this).parent().find(".small_preview.prev").stop(true, true).fadeToggle(400, "linear");
        });
        arrowNext1.hover(function () {
            $(this).parent().find(".small_preview.next").stop(true, true).fadeToggle(400, "linear");
        });


        arrowPrev2.hover(function () {
            if (!$(this).hasClass("disabled")) {
                $(this).parent().parent().parent().find(".small_preview.prev").stop(true, true).fadeToggle(400, "linear");
            }
        });
        arrowNext2.hover(function () {
            if (!$(this).hasClass("disabled")) {
                $(this).parent().parent().parent().find(".small_preview.next").stop(true, true).fadeToggle(400, "linear");
            }
        });
        arrowPrev3.mouseleave(function () {
            jQuery(this).parent().parent().find(".small_preview.prev").stop(true, true).fadeOut(100, "linear");
        });

        arrowNext3.mouseleave(function () {
            jQuery(this).parent().parent().find(".small_preview.next").stop(true, true).fadeOut(100, "linear");
        });

        // PROGRESS BAR
        progressBar.each(function () {

            var val = parseInt(jQuery(this).attr('data-width'), 10),
                len = val + '%';



            jQuery(this).css('width', len);

        });
        // brands block
        brands.jcarousel({
            vertical: false,
            visible: 5,
            scroll: 3,
            buttonNextHTML: '<a class="btn"><i class="icon-right-thin icon-large"></i></a>',
            buttonPrevHTML: '<a class="btn"><i class="icon-left-thin icon-large"></i></a>'
        });
        brandsImg.mouseover(function () {
            brandsImg.removeClass("brands_active").addClass("brands_n_active");
            $(this).removeClass("brands_n_active").addClass("brands_active");
        }).mouseout(function () {
            brandsImg.removeClass("brands_n_active").removeClass("brands_active");
        });

        // modal popup

        var $modalpopup = $('.modal');

        $modalpopup.modal('hide');
        $modalpopup.find('.close').click(function () {
            console.log('close');
            $(this).closest('.modal').modal("hide");
        });


        function GalleryZoomScroll(windowWidth) {
            //console.log('GalleryZoomScroll');
            var elevateZoom = $(".elevate-zoom img.elevatezoom");

            var zoomH = elevateZoom.height();
            var zoomW = elevateZoom.width();
            var zoomPos = 1;
            var zoomType = 'window';
            if (windowWidth < 769) {
                //zoomType = 'inner';
            }
            if (elevateZoom.length != 0) {

                elevateZoom.elevateZoom({
                    responsive: true,
                    easing: false,
                    zoomType: zoomType,
                    cursor: "crosshair",
                    showLens: true,
                    zoomWindowPosition: zoomPos,
                    zoomWindowHeight: zoomH,
                    zoomWindowWidth: zoomW,
                    gallery: "elevate-gallery",
                    galleryActiveClass: 'active',
                    imageCrossfade: true,
                    onZoomedImageLoaded: function () {
                        if (jQuery(".zoomWrapper").length != 0) {

                            var PreviewSliderHeight = function () {
                                var big_image_height = elevateZoom.height();
                                var preview_image_height = $(".elevate-gallery").find('li:first-child').height();
                                var slider_height = Math.round(big_image_height / preview_image_height) * preview_image_height;
                                $(".jcarousel-skin-previews").find('.jcarousel-clip-vertical').css({
                                    "height": slider_height + "px"
                                });
                            };

                            // small thumbnails vertical carousel

                            cloudGalleryOuter.jcarousel({
                                vertical: true,
                                scroll: 3,
                                buttonNextHTML: '<a class="btn"><i class="icon-up"></i></a>',
                                buttonPrevHTML: '<a class="btn"><i class="icon-down"></i></a>',
                                itemLoadCallback: PreviewSliderHeight
                            });
                        }
                    }
                })
            }
            //pass the images to Fancybox
            if ($(".elevate-zoom.fancybox").length != 0) {
                elevateZoom.bind("click", function (e) {
                    console.log('click');
                    var ez = elevateZoom.data('elevateZoom');
                    $.fancybox(ez.getGalleryList());
                    return false;
                });
            }
        }
        GalleryZoomScroll(windowWidth);

        $(window).resize(function () {
            // remove elevateZoom
            var elevateZoom = $(".elevate-zoom img.elevatezoom");
            $('.zoomContainer').each(function () {
                $(this).remove();
                console.log('remove');
            })
            $.removeData(elevateZoom, 'elevateZoom');
            if (elevateZoom.parent('.zoomWrapper').length > 0) {
                var zoom_img = elevateZoom.clone();
                $(".elevate-zoom > .zoomWrapper").replaceWith(zoom_img);
            }
            GalleryZoomScroll(windowWidth);
 			var productsRow = $(".big_with_description");
			productsRow.each(function () {
				calculateProductsInRow(this);
			});
        })


        $(window).scroll(function () {
            <!--SPY-->   
			var windowWidth = window.innerWidth || document.documentElement.clientWidth;         
            if (windowWidth > 767) {

                if ($(this).scrollTop() > $('#header .wrapper_w').height() + 60 + $('#topline').height()) {
                    $('#spy').addClass('fix')
                } else {
                    $('#spy').removeClass('fix');
                }
            }
            if ($(this).scrollTop() > 150) {
                rightToolbar.fadeIn();
            } else {
                if (rightToolbar.find(".shopping_cart_mini").css("display") == "block") {
                    rightToolbar.find(".shopping_cart_mini").fadeOut();
                }
                rightToolbar.fadeOut();
            }
        });

        $(window).resize(function () {
            preview.hide();
            smallPreview.hide();
            shoppingCartMini.hide();
            TopSlider(flexSliderTop);
 			ProductImgHeight();

            if (isotopeOuter.length != 0) {
                isotopeOuter.isotope('reLayout');
            }
            if (hoverfold.length != 0) {
                hoverfold.isotope('reLayout');
            }
            if (ppPicHolder.length != 0) {}
            clearTimeout(resize_picholder);
            resize_picholder = setTimeout(function () {
                resizePicHolder();
            }, 100);
			
			// products carousel arrow visible
			carousel.each(function() {
				if ($(this).find('.product_outer').width() <= $(this).find('.row').width()+10) {
					$(this).find('.es-nav').hide();
				} else $(this).find('.es-nav').show();
			})

        });

    })
    $(window).load(function () {
        var isotopeOuter = $('.isotope-outer');
        var mainNav = $('#nav');
        var spy = $('#spy');
        var cloudGalleryOuter = $('.product-more-views:not(.elevate-gallery) ul');
        var imageCloudZoom = $('.product-image img.cloudzoom');
		
		ProductImgHeight();
        
		if (isotopeOuter.length != 0) {
            var $container = isotopeOuter;
            if (isotopeOuter.find('.product')[0]) {
                $container.isotope({
                    itemSelector: '.product',
                    getSortData: {
                        name: function ($elem) {
                            return $elem.find('.product-name a ').text();
                        },
                        price: function ($elem) {
                            return parseFloat($elem.find('.sort-price').text());
                        },
                        rating: function ($elem) {
                            return parseFloat($elem.find('.sort-rating').text());
                        }
                    }

                });
            }
            if (isotopeOuter.find('.post-preview-small')[0]) {
                $container.isotope({
                    itemSelector: '.post-preview-small'
                });
                var $optionSets = $('#options .option-set'),
                    $optionLinks = $optionSets.find('a');
                $optionLinks.click(function () {
                    var $this = $(this);
                    // don't proceed if already selected
                    if ($this.hasClass('selected')) {
                        return false;
                    }
                    var $optionSet = $this.parents('.option-set');
                    $optionSet.find('.selected').removeClass('selected');
                    $this.addClass('selected');

                    // make option object dynamically, i.e. { filter: '.my-filter-class' }
                    var options = {},
                        key = $optionSet.attr('data-option-key'),
                        value = $this.attr('data-option-value');
                    // parse 'false' as false boolean
                    value = value === 'false' ? false : value;
                    options[key] = value;
                    if (key === 'layoutMode' && typeof changeLayoutMode === 'function') {
                        // changes in layout modes need extra logic
                        changeLayoutMode($this, options)
                    } else {
                        // otherwise, apply new options
                        $container.isotope(options);
                    }
                    return false;
                });
            }
        }
		
		 var $optionSets = jQuery(".filters-by-category .option-set"),
         $optionLinks = $optionSets.find("a");
         
		 $optionLinks.click(function () {
            var $this = $(this);
            if ($this.hasClass("selected")) return false;
            var $optionSet = $this.parents(".option-set");
            $optionSet.find(".selected").removeClass("selected");
            $this.addClass("selected");
            var options = {}, key = $optionSet.attr("data-option-key"),
                value = $this.attr("data-option-value");
            value = value === "false" ? false : value;
            options[key] = value;
            if (key === "layoutMode" && typeof changeLayoutMode === "function") changeLayoutMode($this, options);
            else if (isotopeOuter.length != 0) {isotopeOuter.isotope(options);}
            return false
        });

		
        if (mainNav.length) {
			if ($('#megamenu').length){            
				spy.find('nav').html($('#megamenu').clone());
			}
			else { 
            	spy.find('nav').html($('#nav:first').clone());
			}
            spy.find('nav li').removeClass('hover');
        }
        spy.find('.spyshop').html(jQuery('#cart').clone());


        var PreviewSliderHeight = function () {
            var big_image_height = imageCloudZoom.height();
            var preview_image_height = cloudGalleryOuter.find('li:first-child').height();
            var slider_height = Math.round(big_image_height / preview_image_height) * preview_image_height;
            console.log(big_image_height);
            $(".jcarousel-skin-previews").find('.jcarousel-clip-vertical').css({
                "height": slider_height + "px"
            });
        };

        // small thumbnails vertical carousel

        cloudGalleryOuter.jcarousel({
            vertical: true,
            scroll: 3,
            buttonNextHTML: '<a class="btn"><i class="icon-up"></i></a>',
            buttonPrevHTML: '<a class="btn"><i class="icon-down"></i></a>',
            itemLoadCallback: PreviewSliderHeight
        });


    })


})(jQuery);