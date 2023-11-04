using System;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму приложения
	/// </summary>
	public partial class MafiaHelperForm: Form
		{
		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		public MafiaHelperForm ()
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;
			RDGenerics.LoadWindowDimensions (this);

			this.CancelButton = BExit;
			this.AcceptButton = BBegin;

			LanguageCombo.Items.AddRange (Localization.LanguagesNames);
			try
				{
				LanguageCombo.SelectedIndex = (int)Localization.CurrentLanguage;
				}
			catch
				{
				LanguageCombo.SelectedIndex = 0;
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
			RDGenerics.SaveWindowDimensions (this);
			}

		// Начало игры
		private void BBegin_Click (object sender, EventArgs e)
			{
			// Сворачивание
			this.Hide ();

			// Получение списка игроков
			MafiaPlayersForm mpf = new MafiaPlayersForm ();
			if (mpf.Players.Count > 0)
				{
				MafiaGameForm mgf = new MafiaGameForm (mpf.Players);
				mgf.Dispose ();
				}

			// Восстановление
			mpf.Dispose ();
			this.Show ();
			}
		}
	}
