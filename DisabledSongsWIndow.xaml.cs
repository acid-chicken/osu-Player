﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace osu_Player
{
    /// <summary>
    /// DisabledSongsWIndow.xaml の相互作用ロジック
    /// </summary>
    public partial class DisabledSongsWindow : Window
    {
        public bool IsModified { get; private set; }

        private DispatcherCollection<Song> _songs;
        private MainWindow _instance;
        private Settings _settings;

        public DisabledSongsWindow()
        {
            InitializeComponent();

            _songs = new DispatcherCollection<Song>();
            _instance = MainWindow.GetInstance();
            _settings = _instance.settings;

            SongsList.ItemsSource = _songs;            
            DataContext = this;
        }

        private void Init(object sender, RoutedEventArgs e)
        {
            try
            {
                _songs.Clear();
            
                foreach (var song in _settings.DisabledSongs)
                {
                    _songs.Add(song);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException.GetType().ToString() + "\n"
                        + ex.InnerException.Message + "\n"
                        + ex.InnerException.StackTrace + "\n"
                );
            }
            
            Status.Content = "右クリックメニューから復元できます。";
        }

        private void AddSong(object song)
        {
            _songs.Add((Song)song);
        }

        private void EnableSong(object sender, RoutedEventArgs e)
        {
            var song = (Song)((MenuItem)sender).Tag;

            if (_settings.DisabledSongs.Contains(song))
            {
                IsModified = true;
                
                _settings.DisabledSongs.Remove(song);
                _settings.Write();

                _songs.Remove(song);
            }
            else
            {
                MessageBox.Show("この曲は既に復元されています。", "osu! Player", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}