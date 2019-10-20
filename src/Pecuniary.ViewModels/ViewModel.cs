using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Pecuniary.ViewModels
{
    public class ViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        [JsonIgnore]
        public string EventName { get; set; }
    }
}