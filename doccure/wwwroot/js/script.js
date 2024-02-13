/*
Author       : Dreamguys
Template Name: Doccure - Bootstrap Template
Version      : 1.0
*/

(function ($) {
    "use strict";

    // Stick Sidebar

    if ($(window).width() > 767) {
        if ($('.theiaStickySidebar').length > 0) {
            $('.theiaStickySidebar').theiaStickySidebar({
                // Settings
                additionalMarginTop: 30
            });
        }
    }

    // Sidebar
    if ($(window).width() <= 991) {
        var Sidemenu = function () {
            this.$menuItem = $('.main-nav a');
        };

        function init() {
            var $this = Sidemenu;
            $('.main-nav a').on('click', function (e) {
                if ($(this).parent().hasClass('has-submenu')) {
                    e.preventDefault();
                }
                if (!$(this).hasClass('submenu')) {
                    $('ul', $(this).parents('ul:first')).slideUp(350);
                    $('a', $(this).parents('ul:first')).removeClass('submenu');
                    $(this).next('ul').slideDown(350);
                    $(this).addClass('submenu');
                } else if ($(this).hasClass('submenu')) {
                    $(this).removeClass('submenu');
                    $(this).next('ul').slideUp(350);
                }
            });
            //$('.main-nav li.has-submenu a.active').parents('li:last').children('a:first').addClass('active').trigger('click');
        }

        // Sidebar Initiate
        init();
    }

    // Textarea Text Count

    var maxLength = 100;
    $('#review_desc').on('keyup change', function () {
        var length = $(this).val().length;
        length = maxLength - length;
        $('#chars').text(length);
    });

    // Select 2

    if ($('.select').length > 0) {
        $('.select').select2({
            minimumResultsForSearch: -1,
            width: '100%'
        });
    }

    // Date Time Picker

    if ($('.datetimepicker').length > 0) {
        $('.datetimepicker').datetimepicker({
            format: 'DD/MM/YYYY',
            icons: {
                up: "fas fa-chevron-up",
                down: "fas fa-chevron-down",
                next: 'fas fa-chevron-right',
                previous: 'fas fa-chevron-left'
            }
        });
    }

    // Fancybox Gallery

    if ($('.clinic-gallery a').length > 0) {
        $('.clinic-gallery a').fancybox({
            buttons: [
                "thumbs",
                "close"
            ],
        });
    }

    // Floating Label

    if ($('.floating').length > 0) {
        $('.floating').on('focus blur', function (e) {
            $(this).parents('.form-focus').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
        }).trigger('blur');
    }

    // Mobile menu sidebar overlay

    $('body').append('<div class="sidebar-overlay"></div>');
    $(document).on('click', '#mobile_btn', function () {
        $('main-wrapper').toggleClass('slide-nav');
        $('.sidebar-overlay').toggleClass('opened');
        $('html').addClass('menu-opened');
        return false;
    });

    $(document).on('click', '.sidebar-overlay', function () {
        $('html').removeClass('menu-opened');
        $(this).removeClass('opened');
        $('main-wrapper').removeClass('slide-nav');
    });

    $(document).on('click', '#menu_close', function () {
        $('html').removeClass('menu-opened');
        $('.sidebar-overlay').removeClass('opened');
        $('main-wrapper').removeClass('slide-nav');
    });

    // Mobile Menu

    /*if($(window).width() <= 991){
        mobileSidebar();
    } else {
        $('html').removeClass('menu-opened');
    }*/

    /*function mobileSidebar() {
        $('.main-nav a').on('click', function(e) {
            $('.dropdown-menu').each(function() {
              if($(this).hasClass('show')) {
                  $(this).slideUp(350);
              }
            });
            if(!$(this).next('.dropdown-menu').hasClass('show')) {
                $(this).next('.dropdown-menu').slideDown(350);
            }
        	
        });
    }*/

    // Tooltip
    var days = ["sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday"]

    if ($('[data-toggle="tooltip"]').length > 0) {
        $('[data-toggle="tooltip"]').tooltip();
    }
    // Show Doctor Schedule Timing by Clinic
    $("#clinic_id_slots").change(function () {
        var result = ``
        var ClinicId = $(this).val()
        if (ClinicId == "_") {
            return;
        }
        fetch(`/Clinic/Index/${ClinicId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Error: ' + response.status);
                }
            })
            .then(data => {
                // Handle the response data
                localStorage.setItem('clinic', JSON.stringify(data))

            })
            .catch(error => {
                console.log(error)
            });

        fetch(`/Doctor/ScheduleTiming/GetSlotsofClinic/${ClinicId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Error: ' + response.status);
                }
            })
            .then(data => {
                // Handle the response data
                console.log(data);
                days.forEach((day, index) => {
                    var dayInOrder = data.filter((d) => d.day == index)
                    if (dayInOrder.length == 0) {
                        result += `<div id="slot_${day}" class="tab-pane fade">
								 <h4 class="card-title d-flex justify-content-between">
									 <span>Time Slots</span> 
									 <a class="edit-link ${day} addTimeSlotModel" data-toggle="modal" href="#add_time_slot"><i class="fa fa-plus-circle"></i> Add Slot</a>
									 </h4>
									<p class="text-muted mb-0">Not Available</p>
									</div>`
                    } else {
                        var subResult = ``
                        dayInOrder.forEach((cc) => {
                            subResult += `<div class="doc-slot-list">
<input type="number" class="form-control " name="Id" value=${cc.id}  style="display: none;">
																			${cc.startTime} - ${cc.endTime}
																			<a class="delete_schedule">

																				<i class="fa fa-times"></i>
																			</a>
																		</div>`
                        })
                        result += `<div id="slot_${day}" class="tab-pane fade show active">
																	<h4 class="card-title d-flex justify-content-between">
																		<span>Time Slots</span> 
																		<a class="edit-link ${day} editTimeSlotModel" data-toggle="modal" href="#edit_time_slot"><i class="fa fa-edit mr-1"></i>Edit</a>
																	</h4>
<div class="doc-times">${subResult}</div>
																	

																</div>`
                    }

                })
                $('.schedule-cont').empty()
                $('.schedule-cont').append(result)
            })
            .catch(error => {

                // Handle any errors
                days.forEach((day) => {
                    result += `<div id="slot_${day}" class="tab-pane fade">
								 <h4 class="card-title d-flex justify-content-between">
									 <span>Time Slots</span> 
									 <a class="edit-link ${day} addTimeSlotModel" data-toggle="modal" href="#add_time_slot"><i class="fa fa-plus-circle"></i> Add Slot</a>
									 </h4>
									<p class="text-muted mb-0">Not Available</p>
									</div>`
                })
                $('.schedule-cont').empty()
                $('.schedule-cont').append(result)
            });


    })
    $(document).on('click', '.addTimeSlotModel', function (e) {
        var dayIndex = -1
        var targetAdd = $(e.target)
        days.forEach(function (day, index) {
            if (targetAdd.hasClass(day)) {
                dayIndex = index;
            }
        })

        $('input[name="Day"]').val(`${dayIndex}`)
        $('.daySelected').text(days[dayIndex])
        /*$('.startDate').empty()
        $('.EndDate').empty()
        var clinic = JSON.parse( localStorage.getItem('clinic'))
        var r3=createOptionsTime(clinic['fromTime'], clinic['toTime'], $('#duration').val())
        $('.startDate').append(r3)
        $('.EndDate').append(r3)*/
    });
    $(document).on('click', '.editTimeSlotModel', function (e) {
        setTimeout(() => { },5000)
        var dayIndexEdit = -1
        var ClinicId = $("#clinic_id_slots").val()
        var target = $(e.target)
        days.forEach(function (day, index) {
            if (target.hasClass(day)) {
                dayIndexEdit = index;
            }
        })
        $('input[name="Day"]').val(`${dayIndexEdit}`)
        $('input[name="ClinicId"]').val(`${ClinicId}`)
        $('.daySelected').text(days[dayIndexEdit])

        fetch(`/Doctor/ScheduleTiming/GetTimingSlotofDay/${ClinicId}/${dayIndexEdit}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Error: ' + response.status);
                }
            })
            .then(data => {
                // Handle the response data
                console.log(data);
                var clinic = JSON.parse(localStorage.getItem('clinic'))
                var resultRetured = '<div class="hours-cont-edit">'
                data.forEach((slot, indexSlot) => {
                    resultRetured += `<div class="hours-cont-delete"><div class="row form-row ">
			<div class="col-12 col-md-10">
			<div class="row form-row">
					<div class="col-12 col-md-6">
						<div class="form-group">
<label>Nmber of day of week</label>
				<input type="number" class="form-control" name="scheduleTimings[${indexSlot}].Day" value=${$('input[name="Day"]').val()} readonly>
				<input type="number" class="form-control slotid" name="scheduleTimings[${indexSlot}].Id" value=${slot.id}  style="display: none;">
			</div>
			</div>
				<div class="col-12 col-md-6">
						<div class="form-group">
<label>Clinic Id</label>
				<input type="number" class="form-control" name="scheduleTimings[${indexSlot}].ClinicId" value=${clinic['id']} readonly>
			</div>
			</div>
			</div>
			</div>
			</div>`
                    var optionsResturend = createOptionsTime(clinic['fromTime'], clinic['toTime'], $('#duration').val(), slot.startTime, '')
                    var optionsResturend2 = createOptionsTime(clinic['fromTime'], clinic['toTime'], $('#duration').val(), '', slot.endTime)

                    resultRetured += `<div class="row form-row">
			<div class="col-12 col-md-10">
				<div class="row form-row">
					<div class="col-12 col-md-6">
						<div class="form-group">
							<label>Start Time</label>
							<select class="form-control" name="scheduleTimings[${indexSlot}].StartTime">
								${optionsResturend}
							</select>
						</div>
					</div>
					<div class="col-12 col-md-6">
					<div class="form-group">
							<label>End Time</label>
							<select class="form-control" name="scheduleTimings[${indexSlot}].EndTime">
							${optionsResturend2}
							</select>
						</div>
					</div>
				</div>
			</div>
			<div class="col-12 col-md-2"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>
		</div></div>`
                })
                resultRetured += "	</div>"
                $(".hours-info-v3").empty();
                $(".hours-info-v3").append(resultRetured);
            })
        return true;

    })
    function createOptionsTime(startTime, endTime, diff, selectedStartTime = '', selectedEndTime = '') {
        var options = ``
        var dateX = new Date();
        var dateY = new Date();

        // Set the hours and minutes for x
        var [hoursX, minutesX] = startTime.split(":");
        dateX.setFullYear(2023, 12, 12)
        dateX.setHours(parseInt(hoursX, 10));
        dateX.setMinutes(parseInt(minutesX, 10));

        var [hoursY, minutesY] = endTime.split(":");
        dateY.setFullYear(2023, 12, 12)

        dateY.setHours(parseInt(hoursY, 10));
        dateY.setMinutes(parseInt(minutesY, 10));
        if (dateX >= dateY) {
            console.log("x is already greater than or equal to y");
        } else {
            // Keep adding 15 minutes to x until it reaches or exceeds y
            while (dateX < dateY) {
                var resultHours = dateX.getHours().toString().padStart(2, "0");
                var resultMinutes = dateX.getMinutes().toString().padStart(2, "0");
                var result2 = resultHours + ":" + resultMinutes;
                if (selectedEndTime == result2 || selectedStartTime == result2) {
                    options += `<option value='${result2}' selected>${result2}</option>`

                } else {
                    options += `<option value='${result2}'>${result2}</option>`

                }
                options += `<option value='${result2}'>${result2}</option>`
                dateX.setTime(dateX.getTime() + diff * 60 * 1000);
            }
            var resultHours = dateY.getHours().toString().padStart(2, "0");
            var resultMinutes = dateY.getMinutes().toString().padStart(2, "0");
            var result2 = resultHours + ":" + resultMinutes;

            if (selectedEndTime == result2 || selectedStartTime == result2) {
                options += `<option value='${result2}' selected>${result2}</option>`

            } else {
                options += `<option value='${result2}'>${result2}</option>`

            }

        }
        return options


    }
    $(document).on('click', '.doc-times', function (e) {
        var slotListJs = $(e.target).closest('.doc-slot-list');
        var SlotListID = slotListJs.find(`input[name="Id"]`).val()
        if (SlotListID !== undefined) {
            $.ajax({
                url: `/Doctor/ScheduleTiming/DeleteSlotById/${SlotListID}`,
                type: 'DELETE',
                contentType: false,
                processData: false,
                cache: false,
                xhrFields: {
                    withCredentials: true
                },
                success: function (response) {
                    // Handle the success response
                    slotListJs.remove();
                    console.log('Slot data sent successfully.');
                    // Perform any additional actions on success, such as showing a success message or redirecting to another page
                },
                error: function (xhr, textStatus, errorThrown) {
                    // Handle the error response
                    console.error('Failed to send Slot data.');
                    // Handle the error case, such as showing an error message to the user
                }
            });
        } 
        return false;
    })
    // Add More Hours
    $(".hours-info-v3").on('click', '.trash', function () {
        var slotJs = $(this).closest('.hours-cont-delete');
        var SlotID = slotJs.find(`input[type="number"].slotid`).val()
        if (SlotID !== undefined) {
            $.ajax({
                url: `/Doctor/ScheduleTiming/DeleteSlotById/${SlotID}`,
                type: 'DELETE',
                contentType: false,
                processData: false,
                cache: false,
                xhrFields: {
                    withCredentials: true
                },
                success: function (response) {
                    // Handle the success response
                    slotJs.remove();
                    console.log('Slot data sent successfully.');
                    // Perform any additional actions on success, such as showing a success message or redirecting to another page
                },
                error: function (xhr, textStatus, errorThrown) {
                    // Handle the error response
                    console.error('Failed to send Slot data.');
                    // Handle the error case, such as showing an error message to the user
                }
            });
        } else {
            slotJs.remove();
        }
        return false;
    });
    $(".hours-info-v2").on('click', '.trash', function () {
        $(this).closest('.hours-cont').remove();
        return false;
    });
    $(".add-hours").on('click', function () {
        var indexHoursInfo = $(".hours-cont").length;

        var clinic = JSON.parse(localStorage.getItem('clinic'))
        var r3 = createOptionsTime(clinic['fromTime'], clinic['toTime'], $('#duration').val())
        var hourscontent = '<div class="hours-cont">'
        hourscontent += `<div class="row form-row ">
			<div class="col-12 col-md-10">
			<div class="row form-row">
					<div class="col-12 col-md-6">
						<div class="form-group">
<label>Nmber of day of week</label>
				<input type="number" class="form-control" name="scheduleTimings[${indexHoursInfo}].Day" value=${$('input[name="Day"]').val()} readonly>
			</div>
			</div>
				<div class="col-12 col-md-6">
						<div class="form-group">
<label>Clinic Id</label>
				<input type="number" class="form-control" name="scheduleTimings[${indexHoursInfo}].ClinicId" value=${clinic['id']} readonly>
			</div>
			</div>
			</div>
			</div>
			</div>`
        hourscontent += `<div class="row form-row">
			<div class="col-12 col-md-10">
				<div class="row form-row">
					<div class="col-12 col-md-6">
						<div class="form-group">
							<label>Start Time</label>
							<select class="form-control" name="scheduleTimings[${indexHoursInfo}].StartTime">
								${r3}
							</select>
						</div>
					</div>
					<div class="col-12 col-md-6">
					<div class="form-group">
							<label>End Time</label>
							<select class="form-control" name="scheduleTimings[${indexHoursInfo}].EndTime">
							${r3}
							</select>
						</div>
					</div>
				</div>
			</div>
			<div class="col-12 col-md-2"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>
		</div>`;
        hourscontent += "	</div>"
        $(".hours-info-v2").append(hourscontent);
        return false;
    });
    $(".add-hours-edit").on('click', function () {
        var indexHoursInfo = $(".hours-cont-edit").length;

        var clinic = JSON.parse(localStorage.getItem('clinic'))
        var r3 = createOptionsTime(clinic['fromTime'], clinic['toTime'], $('#duration').val())
        var hourscontent = '<div class="hours-cont-edit">'
        hourscontent += `<div class="row form-row ">
			<div class="col-12 col-md-10">
			<div class="row form-row">
					<div class="col-12 col-md-6">
						<div class="form-group">
<label>Nmber of day of week</label>
				<input type="number" class="form-control" name="scheduleTimings[${indexHoursInfo}].Day" value=${$('input[name="Day"]').val()} readonly>
			</div>
			</div>
				<div class="col-12 col-md-6">
						<div class="form-group">
<label>Clinic Id</label>
				<input type="number" class="form-control" name="scheduleTimings[${indexHoursInfo}].ClinicId" value=${clinic['id']} readonly>
			</div>
			</div>
			</div>
			</div>
			</div>`
        hourscontent += `<div class="row form-row">
			<div class="col-12 col-md-10">
				<div class="row form-row">
					<div class="col-12 col-md-6">
						<div class="form-group">
							<label>Start Time</label>
							<select class="form-control" name="scheduleTimings[${indexHoursInfo}].StartTime">
								${r3}
							</select>
						</div>
					</div>
					<div class="col-12 col-md-6">
					<div class="form-group">
							<label>End Time</label>
							<select class="form-control" name="scheduleTimings[${indexHoursInfo}].EndTime">
							${r3}
							</select>
						</div>
					</div>
				</div>
			</div>
			<div class="col-12 col-md-2"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>
		</div>`;
        hourscontent += "	</div>"
        $(".hours-info-v3").append(hourscontent);
        return false;
    });

    // Content div min height set

    function resizeInnerDiv() {
        var height = $(window).height();
        var header_height = $(".header").height();
        var footer_height = $(".footer").height();
        var setheight = height - header_height;
        var trueheight = setheight - footer_height;
        $(".content").css("min-height", trueheight);
    }

    if ($('.content').length > 0) {
        resizeInnerDiv();
    }

    $(window).resize(function () {
        if ($('.content').length > 0) {
            resizeInnerDiv();
        }
        /*if($(window).width() <= 991){
            mobileSidebar();
        } else {
            $('html').removeClass('menu-opened');
        }*/
    });

    // Slick Slider

    if ($('.specialities-slider').length > 0) {
        $('.specialities-slider').slick({
            dots: true,
            autoplay: false,
            infinite: true,
            variableWidth: true,
            prevArrow: false,
            nextArrow: false
        });
    }

    if ($('.doctor-slider').length > 0) {
        $('.doctor-slider').slick({
            dots: false,
            autoplay: false,
            infinite: true,
            variableWidth: true,
        });
    }
    if ($('.features-slider').length > 0) {
        $('.features-slider').slick({
            dots: true,
            infinite: true,
            centerMode: true,
            slidesToShow: 3,
            speed: 500,
            variableWidth: true,
            arrows: false,
            autoplay: false,
            responsive: [{
                breakpoint: 992,
                settings: {
                    slidesToShow: 1
                }

            }]
        });
    }

    // Date Time Picker

    if ($('.datepicker').length > 0) {
        $('.datepicker').datetimepicker({
            viewMode: 'years',
            showTodayButton: true,
            format: 'DD-MM-YYYY',
            // minDate:new Date(),
            widgetPositioning: {
                horizontal: 'auto',
                vertical: 'bottom'
            }
        });
    }
    //choose booking time
    $('.timing').on("click", function () {
        $('.timing').removeClass('selected')
        $(this).addClass('selected')
        var tempp = $(this).find(`input[name="Idtemp"]`).val()
        var tempp2 = $(this).find(`input[name="BookingDateTemp"]`).val()
        console.log(tempp)
        $('input[name=Id]').val(tempp)
        $('input[name=BookingDate]').val(tempp2)
        console.log("timing selected")
    })
    // Chat

    var chatAppTarget = $('.chat-window');
    (function () {
        if ($(window).width() > 991)
            chatAppTarget.removeClass('chat-slide');

        $(document).on("click", ".chat-window .chat-users-list a.media", function () {
            if ($(window).width() <= 991) {
                chatAppTarget.addClass('chat-slide');
            }
            return false;
        });
        $(document).on("click", "#back_user_list", function () {
            if ($(window).width() <= 991) {
                chatAppTarget.removeClass('chat-slide');
            }
            return false;
        });
    })();

    // Circle Progress Bar

    function animateElements() {
        $('.circle-bar1').each(function () {
            var elementPos = $(this).offset().top;
            var topOfWindow = $(window).scrollTop();
            var percent = $(this).find('.circle-graph1').attr('data-percent');
            var animate = $(this).data('animate');
            if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
                $(this).data('animate', true);
                $(this).find('.circle-graph1').circleProgress({
                    value: percent / 100,
                    size: 400,
                    thickness: 30,
                    fill: {
                        color: '#da3f81'
                    }
                });
            }
        });
        $('.circle-bar2').each(function () {
            var elementPos = $(this).offset().top;
            var topOfWindow = $(window).scrollTop();
            var percent = $(this).find('.circle-graph2').attr('data-percent');
            var animate = $(this).data('animate');
            if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
                $(this).data('animate', true);
                $(this).find('.circle-graph2').circleProgress({
                    value: percent / 100,
                    size: 400,
                    thickness: 30,
                    fill: {
                        color: '#68dda9'
                    }
                });
            }
        });
        $('.circle-bar3').each(function () {
            var elementPos = $(this).offset().top;
            var topOfWindow = $(window).scrollTop();
            var percent = $(this).find('.circle-graph3').attr('data-percent');
            var animate = $(this).data('animate');
            if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
                $(this).data('animate', true);
                $(this).find('.circle-graph3').circleProgress({
                    value: percent / 100,
                    size: 400,
                    thickness: 30,
                    fill: {
                        color: '#1b5a90'
                    }
                });
            }
        });
    }

    if ($('.circle-bar').length > 0) {
        animateElements();
    }
    $(window).scroll(animateElements);

})(jQuery);