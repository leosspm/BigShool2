﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BitSchool.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value)
            , "M/d/yyyy",
            CultureInfo.CurrentCulture,
            DateTimeStyles.None,
            out  dateTime);

            return (isValid && dateTime > DateTime.Now);
        }
    }
}