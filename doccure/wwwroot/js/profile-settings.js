/*
Author       : Dreamguys
Template Name: Doccure - Bootstrap Template
Version      : 1.0
*/

(function($) {
    "use strict";
	
	// Pricing Options Show
	
	$('#pricing_select input[name="rating_option"]').on('click', function() {
		if ($(this).val() == 'price_free') {
			$('#custom_price_cont').hide();
		}
		if ($(this).val() == 'custom_price') {
			$('#custom_price_cont').show();
		}
		else {
		}
	});
	
	// Education Add More
	
    $(".education-info").on('click','.trash', function () {
		var education = $(this).closest('.education-cont')//.remove();
		var EducationID = education.find(`input[type="text"].EducationID`).val()
		if (EducationID !== undefined) {
			$.ajax({
				url: `/DeleteEducation/DeleteEducation/${EducationID}`,
				type: 'DELETE',
				contentType: false,
				processData: false,
				cache: false,
				xhrFields: {
					withCredentials: true
				},
				success: function (response) {
					// Handle the success response
					education.remove();
					console.log('Education data sent successfully.');
					// Perform any additional actions on success, such as showing a success message or redirecting to another page
				},
				error: function (xhr, textStatus, errorThrown) {
					// Handle the error response
					console.error('Failed to send education data.');
					// Handle the error case, such as showing an error message to the user
				}
			});
		} else {
			education.remove();
		}

		return false;
    });
	$(".add-education").on('click', function () {
		var indexEducation = $(".education-cont").length;

		var educationcontent = `<div class="row form-row education-cont">
			<div class="col-12 col-md-10 col-lg-11">
				<div class="row form-row">
					<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>Degree</label>
							<input  name="Educations[${indexEducation}].Degree"  type="text" class="form-control">
						</div>
					</div>
					<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>College/Institute</label>
							<input name="Educations[${indexEducation}].College" type="text" class="form-control">
						</div>
					</div>
					<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>Year of Completion</label>
							<input name="Educations[${indexEducation}].YearofCompletion" type="date" class="form-control">
						</div>
					</div>
			</div>
			</div>
			<div class="col-12 col-md-2 col-lg-1"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>
		</div>`;
		
        $(".education-info").append(educationcontent);
        return false;
    });
	
	// Experience Add More
	
	$(".experience-info").on('click','.trash', function () {
		var experience = $(this).closest('.experience-cont')//.remove();
		var ExperienceID = experience.find(`input[type="text"].ExperienceID`).val()
		if (ExperienceID !== undefined) {
			$.ajax({
				url: `/DeleteExperience/DeleteExperience/${ExperienceID}`,
				type: 'DELETE',
				contentType: false,
				processData: false,
				cache: false,
				xhrFields: {
					withCredentials: true
				},
				success: function (response) {
					// Handle the success response
					experience.remove();
					console.log('experience data sent successfully.');
					// Perform any additional actions on success, such as showing a success message or redirecting to another page
				},
				error: function (xhr, textStatus, errorThrown) {
					// Handle the error response
					console.error('Failed to send experience data.');
					// Handle the error case, such as showing an error message to the user
				}
			});
		} else {
			experience.remove();
		}
		return false;
    });

    $(".add-experience").on('click', function () {
		var indexExperience = $(".experience-cont").length;

		var experiencecontent = `<div class="row form-row experience-cont">
			<div class="col-12 col-md-10 col-lg-11">
				<div class="row form-row">
					<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>Hospital Name</label>
							<input type="text" class="form-control" name="Experience[${indexExperience}].HospitalName">
						</div>
					</div>
					<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>From</label>
							<input type="date" class="form-control" name="Experience[${indexExperience}].From">
						</div>
					</div>
						<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>To</label>
							<input name="Experience[${indexExperience}].To" type="date" class="form-control" >
						</div>
					</div>
					<div class="col-12 col-md-6 col-lg-4">
						<div class="form-group">
							<label>Designation</label>
							<input type="text" class="form-control" name="Experience[${indexExperience}].Designation">
						</div>
					</div>
				</div>
			</div>
			<div class="col-12 col-md-2 col-lg-1"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>
		</div>`;
		
        $(".experience-info").append(experiencecontent);
        return false;
    });
	
	// Awards Add More
	
    $(".awards-info").on('click','.trash', function () {
		var award = $(this).closest('.awards-cont')//.remove();
		var AwardId = award.find(`input[type="text"].AwardId`).val()
		if (AwardId !== undefined) {
			$.ajax({
				url: `/DeleteAward/DeleteAward/${AwardId}`,
				type: 'DELETE',
				contentType: false,
				processData: false,
				cache: false,
				xhrFields: {
					withCredentials: true
				},
				success: function (response) {
					// Handle the success response
					award.remove();
					console.log('Award data sent successfully.');
					// Perform any additional actions on success, such as showing a success message or redirecting to another page
				},
				error: function (xhr, textStatus, errorThrown) {
					// Handle the error response
					console.error('Failed to send Award data.');
					// Handle the error case, such as showing an error message to the user
				}
			});
		} else {
			award.remove();
		}
		return false;
    });

    $(".add-award").on('click', function () {
		var indexAwards = $(".awards-cont").length;

        var regcontent = `<div class="row form-row awards-cont">
			<div class="col-12 col-md-5">
				<div class="form-group">
					<label>Awards</label>
					<input name="Awards[${indexAwards}].award" type="text" class="form-control">
				</div>
			</div>
			<div class="col-12 col-md-5">
				<div class="form-group">
					<label>Year</label>
					<input name="Awards[${indexAwards}].year" type="date" class="form-control">
				</div>
			</div>
			<div class="col-12 col-md-2">
				<label class="d-md-block d-sm-none d-none">&nbsp;</label>
				<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>
			</div>
		</div>`;
		
        return false;
    });
	
	// Membership Add More
	
    $(".membership-info").on('click','.trash', function () {
		var membership = $(this).closest('.membership-cont')//.remove();
		var MembershipId = membership.find(`input[type="text"].MembershipId`).val()
		if (MembershipId !== undefined) {
			$.ajax({
				url: `/DeleteMembership/DeleteMembership/${MembershipId}`,
				type: 'DELETE',
				contentType: false,
				processData: false,
				cache: false,
				xhrFields: {
					withCredentials: true
				},
				success: function (response) {
					// Handle the success response
					membership.remove();
					console.log('Membership data sent successfully.');
					// Perform any additional actions on success, such as showing a success message or redirecting to another page
				},
				error: function (xhr, textStatus, errorThrown) {
					// Handle the error response
					console.error('Membership to send Award data.');
					// Handle the error case, such as showing an error message to the user
				}
			});
		} else {
			membership.remove();
		}
		return false;
    });

    $(".add-membership").on('click', function () {
		var indexMembership = $(".membership-cont").length;

        var membershipcontent = `<div class="row form-row membership-cont">
			<div class="col-12 col-md-10 col-lg-5">
				<div class="form-group">
					<label>Memberships</label>
					<input name="Membership[${indexMembership}].description" type="text" class="form-control">
				</div>
			</div>
			<div class="col-12 col-md-2 col-lg-2">
				<label class="d-md-block d-sm-none d-none">&nbsp;</label>
				<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>
			</div>
		</div>`;
		
        $(".membership-info").append(membershipcontent);
        return false;
    });
	
	// Registration Add More
	
    $(".registrations-info").on('click','.trash', function () {
		$(this).closest('.reg-cont').remove();
		return false;
    });

    $(".add-reg").on('click', function () {

        var regcontent = '<div class="row form-row reg-cont">' +
			'<div class="col-12 col-md-5">' +
				'<div class="form-group">' +
					'<label>Registrations</label>' +
					'<input type="text" class="form-control">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-5">' +
				'<div class="form-group">' +
					'<label>Year</label>' +
					'<input type="text" class="form-control">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-2">' +
				'<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
				'<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
			'</div>' +
		'</div>';
		
        $(".registrations-info").append(regcontent);
        return false;
    });
	
})(jQuery);