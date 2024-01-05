using System;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму приложения
	/// </summary>
	public partial class MafiaHelperForm: Form
		{
		/// <summary>
		/// Возвращает путь к звуку завершения таймера
		/// </summary>
		public static string EndTone
			{
			get
				{
				return RDGenerics.AppStartupPath + "End.mp3";
				}
			}

		/// <summary>
		/// Возвращает путь к звуку реперной точки таймера
		/// </summary>
		public static string MidTone
			{
			get
				{
				return RDGenerics.AppStartupPath + "Mid.mp3";
				}
			}

		/// <summary>
		/// Возвращает путь к звуку отдельного гудка
		/// </summary>
		public static string LongTone
			{
			get
				{
				return RDGenerics.AppStartupPath + "Long.mp3";
				}
			}

		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		public MafiaHelperForm ()
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;
			this.CancelButton = BExit;
			this.AcceptButton = BBegin;

			// Загрузка настроек
			RDGenerics.LoadWindowDimensions (this);
			MusicPath.Text = MafiaSettings.MusicPath;
			TimerEndMessage.Checked = MafiaSettings.TimerMessage;
			TimerSound.Checked = MafiaSettings.TimerSounds;
			try
				{
				NonDefaultRolesNight.Value = MafiaSettings.NonDefaultRolesNight;
				}
			catch { }

			LanguageCombo.Items.AddRange (Localization.LanguagesNames);
			try
				{
				LanguageCombo.SelectedIndex = (int)Localization.CurrentLanguage;
				}
			catch
				{
				LanguageCombo.SelectedIndex = 0;
				}

			// Попытка создания ресурсов приложения
			try
				{
				if (!File.Exists (EndTone))
					File.WriteAllBytes (EndTone, Properties.MafiaHelper.EndTone);
				if (!File.Exists (MidTone))
					File.WriteAllBytes (MidTone, Properties.MafiaHelper.MidTone);
				if (!File.Exists (LongTone))
					File.WriteAllBytes (LongTone, Properties.MafiaHelper.LongTone);
				}
			catch
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					Localization.GetDefaultText (LzDefaultTextValues.Message_PackageSavingFailure));
				}
			}

		// Локализация формы
		private void LanguageCombo_SelectedIndexChanged (object sender, EventArgs e)
			{
			// Сохранение языка
			Localization.CurrentLanguage = (SupportedLanguages)LanguageCombo.SelectedIndex;

			BExit.Text = Localization.GetDefaultText (LzDefaultTextValues.Button_Exit);
			AboutButton.Text = Localization.GetDefaultText (LzDefaultTextValues.Control_AppAbout);
			BBegin.Text = Localization.GetText ("BBegin");

			MainPage.Text = BBegin.Text;
			SettingsPage.Text = Localization.GetText ("BSettings");
			MusicPathLabel.Text = FBDialog.Description = Localization.GetText ("MusicPathLabel");

			TimerSettingsLabel.Text = Localization.GetText ("TimerSettingsLabel");
			TimerEndMessage.Text = Localization.GetText ("TimerEndMessage");
			TimerSound.Text = Localization.GetText ("TimerSound");

			NonDefaultRolesLabel.Text = Localization.GetText ("NonDefaultRolesLabel");

			LanguageLabel.Text = Localization.GetDefaultText (LzDefaultTextValues.Control_InterfaceLanguage);
			}

		// Запрос справки
		private void AboutButton_Clicked (object sender, EventArgs e)
			{
			RDGenerics.ShowAbout (false);
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MafiaHelperForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			// Сохранение настроек
			SaveSettings ();
			RDGenerics.SaveWindowDimensions (this);
			}

		// Начало игры
		private void BBegin_Click (object sender, EventArgs e)
			{
			// Сохранение настроек
			SaveSettings ();
			RDGenerics.SaveWindowDimensions (this);

			// Сворачивание
			this.Hide ();

			// Получение списка игроков
			MafiaPlayersForm mpf = new MafiaPlayersForm ();
			if (mpf.Players.Count > 0)
				{
				MafiaGameForm mgf = new MafiaGameForm (mpf.Players, mpf.PlayersRolesOrder);
				mgf.Dispose ();
				}

			// Восстановление
			mpf.Dispose ();
			this.Show ();
			}

		// Сохранение настроек
		private void SaveSettings ()
			{
			MafiaSettings.MusicPath = MusicPath.Text;
			MafiaSettings.TimerSounds = TimerSound.Checked;
			MafiaSettings.TimerMessage = TimerEndMessage.Checked;
			MafiaSettings.NonDefaultRolesNight = (uint)NonDefaultRolesNight.Value;
			}

		// Выбор директории с музыкой
		private void MusicPathButton_Click (object sender, EventArgs e)
			{
			FBDialog.SelectedPath = MusicPath.Text;
			if (FBDialog.ShowDialog () == DialogResult.OK)
				MusicPath.Text = FBDialog.SelectedPath;
			}
		}
	}
