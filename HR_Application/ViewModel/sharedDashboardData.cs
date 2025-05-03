using HR_Application.Model;
using HR_Application.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.ViewModel
{
    public class sharedDashboardData : INotifyPropertyChanged
    {
        private static sharedDashboardData _instance;
        public static sharedDashboardData Instance => _instance ??= new sharedDashboardData();

        private DashboardInfoModel _dashboardInfo;
        public DashboardInfoModel DashboardInfo
        {
            get => _dashboardInfo;
            private set
            {
                _dashboardInfo = value;
                OnPropertyChanged(nameof(DashboardInfo));
            }
        }

        private readonly IEmployeeRepository _repo;

        private sharedDashboardData()
        {
            _repo = new EmployeeRepository();
            LoadData();
        }

        public void Refresh()
        {
            LoadData(); // re-fetch from DB
        }

        private void LoadData()
        {
            DashboardInfo = _repo.GetDashboardInfo();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
