﻿using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;



namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeOverview
    {
        public IEnumerable<Employee> Employees { get; set; }


        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }


        protected async override Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }
    }
}
