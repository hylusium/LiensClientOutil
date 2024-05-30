using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;


namespace ToolClientOutil.Models
{
	public class ViewModelLogin
	{
		[Required(ErrorMessage = "L'adresse Mail est obligatoire")]
		
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage ="Le mot de passe est obligatoire")]
		public string Password { get; set; }
	}
}