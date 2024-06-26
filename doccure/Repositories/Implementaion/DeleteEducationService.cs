﻿using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DeleteEducationService : IDeleteEducation
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DeleteEducationService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<bool> DeleteEducationAsync(int id, ClaimsPrincipal user)
		{
			var User = await userManager.Users.Include(a => a.doctor)
									.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			var education = await applicationDbContext.Education.FirstOrDefaultAsync(e=>e.Id==id);
			if(User == null||education==null)
			{
				return false;
			}
			else
			{
				if(education.DoctorId== User.doctor.Id)
				{
					applicationDbContext.Remove(education);
					await applicationDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}
				

			}
		}
	}
}
