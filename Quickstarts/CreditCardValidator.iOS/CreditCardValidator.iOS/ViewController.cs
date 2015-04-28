﻿using System;

using UIKit;
using CreditCardValidation.Common;

namespace CreditCardValidator.iOS
{
	public partial class ViewController : UIViewController
	{
		static readonly ICreditCardValidator _validator = new SimpleCreditCardValidator();

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void ValidateButton_TouchUpInside(UIButton sender)
		{
			ErrorMessagesTextField.Text = String.Empty;
			string errorMessage;
			bool isValid = _validator.IsCCValid(CreditCardTextField.Text, out errorMessage);


			if(isValid)
			{
				UIViewController ctlr = this.Storyboard.InstantiateViewController("ValidCreditCardController");
				NavigationController.PushViewController(ctlr, true);
			} else
			{
				InvokeOnMainThread(() => {
						CreditCardTextField.BackgroundColor = UIColor.Yellow;
						CreditCardTextField.Layer.BorderColor = UIColor.Red.CGColor;
						CreditCardTextField.Layer.BorderWidth = 2;
						CreditCardTextField.Layer.CornerRadius = 5;
						ErrorMessagesTextField.Text = errorMessage;					
					});
			}

		}
	}
}

