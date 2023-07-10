using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BethanysPieShopHRM.App.Pages
{
	public partial class EmployeeEdit
    {
		[Inject]
		public IEmployeeDataService EmployeeDataService { get; set; }


		[Inject]
		public ICountryDataService CountryDataService { get; set; }


		[Inject]
		public IJobCategoryDataService JobCategoryDataService { get; set; }


		[Parameter]
		public string EmployeeId { get; set; }


		public Employee Employee { get; set; } = new Employee();


		public List<Country> Countries { get; set; } = new List<Country>();


		public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();


		protected string CountryId = string.Empty;
		protected string JobCategoryId = string.Empty;


		// used to store state of scream
		protected string Message = string.Empty;
		protected string StatusClass = string.Empty;
		protected bool Saved;


		protected override async Task OnInitializedAsync()
		{
			Saved = false;

			Countries = (await CountryDataService.GetAllCountries()).ToList();
			JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();


			int.TryParse(EmployeeId, out var employeeId);

			if (employeeId == 0)
			{
				// add some defaults
				Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
			}
			else
			{
				Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
			}

			CountryId = Employee.CountryId.ToString();
			JobCategoryId = Employee.JobCategoryId.ToString();
		}


		protected async Task HandleValidSubmit()
		{
			Saved = false;

			Employee.CountryId = int.Parse(CountryId);
			Employee.JobCategoryId = int.Parse(JobCategoryId);

			if (Employee.EmployeeId == 0)
			{
				var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
				if (addedEmployee != null)
				{
					StatusClass = "alert-success";
                    Message = "New employee added successfuly.";
					Saved = true;
				}
				else
				{
					StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
					Saved = false;
				}
			}
			else
			{
				await EmployeeDataService.UpdateEmployee(Employee);
				StatusClass = "alert-success";
                Message = "Employee updated successfuly.";
				Saved = true;
			}
		}


		protected void HandleInvalidSubmit()
		{
			StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
		}

	}
}
