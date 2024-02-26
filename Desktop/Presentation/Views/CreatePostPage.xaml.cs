﻿using Desktop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PosteServiceDekstop = Desktop.Infrastructure.Services.PosteService;

namespace Desktop.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour CreatePostPage.xaml
    /// </summary>
    public partial class CreatePostPage : Page
    {
        private readonly PosteServiceDekstop _posteServiceDekstop;
        private readonly DeviceInfoService _deviceInfoService;

        public List<TypeModele> TypesList = new List<TypeModele>();
        public List<TypeModele> SallesList = new List<TypeModele>();

        public TypeModele typeModele = new TypeModele();

        public CreatePostPage()
        {
            InitializeComponent();

            _posteServiceDekstop = new PosteServiceDekstop();
            _deviceInfoService = new DeviceInfoService();

            GetTypesAndSalles();

        }

        public async void GetTypesAndSalles()
        {
            var types = await _posteServiceDekstop.GetTypes();
            var salles = await _posteServiceDekstop.GetSalles();

            if (types != null)
                cmb_types.ItemsSource = types;

            if (salles != null)
                cmb_salles.ItemsSource = salles;
        }

        public void GetDeviceInnfo()
        {

        }

    }
}
