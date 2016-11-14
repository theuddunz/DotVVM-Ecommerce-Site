using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace EntityFrameworkCF.ViewModels
{
	public class MasterpageViewModel : DotvvmViewModelBase
	{
        public void SignOut()
        {
            Context.OwinContext.Authentication.SignOut();
            Context.RedirectToRoute("LoginPage");
        }
	}
}

