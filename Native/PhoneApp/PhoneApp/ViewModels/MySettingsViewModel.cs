using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
namespace PhoneApp
{
	public class MySettingsViewModel: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ICommand SaveCommand { protected set; get; }
		public ICommand CancelCommand { protected set; get; }
		LocalData _db=new LocalData();
		public MySettingsViewModel ()
		{
			var user=_db.GetUserInfo ();
			if (user != null) {
				id = user.Id;
				lastName = user.LastName;
				firstName = user.FirstName;
				email = user.Email;
				phone = user.Phone;
				mobile = user.Mobile;
				receiveEmail = user.ReceiveEmail;
				receiveNotification = user.ReceiveNotification;
			}

			this.SaveCommand = new Command(() =>
				{
					if(SaveUser()>0){
						ReturnMessage="Saved";
                    }
				});
			this.CancelCommand = new Command(() =>
				{
					LastName=null;
					FirstName=null;
					Email=null;
					Phone=null;
					Mobile=null;
					ReceiveEmail=false;
					ReceiveNotification=false;
					if(SaveUser()>0){
						ReturnMessage="Canceled";
					}
				});
		}
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}


		int id;
		string lastName,firstName,email,phone,mobile,returnMessage;
		bool receiveEmail,receiveNotification;

		public string ReturnMessage { get{ return returnMessage; } set{
				if (returnMessage != value)
				{
					returnMessage = value;
					OnPropertyChanged("ReturnMessage");
				}
			}
		}

		public int Id { get{ return id; } set{
				if (id != value)
				{
					id = value;
					OnPropertyChanged("Id");
				} 
			} 
		}
		public string LastName { get{ return lastName; } set{
				if (lastName != value)
				{
					lastName = value;
					OnPropertyChanged("LastName");
				} 
			} 
		}
		public string FirstName { get{ return firstName; } set{
				if (firstName != value)
				{
					firstName = value;
					OnPropertyChanged("FirstName");
				} 
			} 
		}
		public string Email { get{ return email; } set{
				if (email != value)
				{
					email = value;
					OnPropertyChanged("Email");
				} 
			} 
		}
		public string Phone { get{ return phone; } set{
				if (phone != value)
				{
					phone = value;
					OnPropertyChanged("Phone");
				} 
			} 
		}
		public string Mobile { get{ return mobile; } set{
				if (mobile != value)
				{
					mobile = value;
					OnPropertyChanged("Mobile");
				} 
			} 
		}
		public bool ReceiveEmail{ get{ return receiveEmail; } set{
				if (receiveEmail != value)
				{
					receiveEmail = value;
					OnPropertyChanged("ReceiveEmail");
				} 
			} 
		}
		public bool ReceiveNotification{ get{ return receiveNotification; } set{
				if (receiveNotification != value)
				{
					receiveNotification = value;
					OnPropertyChanged("ReceiveNotification");
				} 
			} 
		}
		private int SaveUser(){
			return _db.SaveUserInfo(new UserInfo(){Id=Id,LastName=LastName,FirstName=FirstName,Email=Email,Phone=Phone,Mobile=Mobile,ReceiveEmail=ReceiveEmail,ReceiveNotification=ReceiveNotification});
		}
	}
}

