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
		$(this).closest('.education-cont').remove();
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
		$(this).closest('.experience-cont').remove();
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
		$(this).closest('.awards-cont').remove();
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
		$(this).closest('.membership-cont').remove();
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