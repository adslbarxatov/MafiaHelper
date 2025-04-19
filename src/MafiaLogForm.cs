using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает форму просмотра журнала приложения
	/// </summary>
	public partial class MafiaLogForm: Form
		{
		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		/// <param name="LogFilePath">Путь к файлу журнала</param>
		public MafiaLogForm (string LogFilePath)
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;

			this.CancelButton = BExit;
			BExit.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Close);

			// Загрузка настроек
			RDGenerics.LoadWindowDimensions (this);

			// Получение текста журнала
			try
				{
				LogText.Text = File.ReadAllText (LogFilePath, RDGenerics.GetEncoding (RDEncodings.UTF8));
				}
			catch { }

			// Форматирование
			int start, end;
			for (int i = 0; i < LogText.Lines.Length - 1; i++)
				{
				start = LogText.GetFirstCharIndexFromLine (i);
				end = LogText.GetFirstCharIndexFromLine (i + 1);

				if (LogText.Lines[i].StartsWith ('['))
					{
					LogText.Select (start, end - start);

					LogText.SelectionFont = new Font (LogText.Font, FontStyle.Bold);
					if (LogText.Lines[i].Contains ('!'))
						LogText.SelectionColor = Color.OrangeRed;
					else
						LogText.SelectionColor = Color.DarkBlue;

					LogText.DeselectAll ();
					continue;
					}

				if (LogText.Lines[i].Contains (':'))
					{
					end = LogText.Lines[i].IndexOf (':');
					LogText.Select (start, end + 1);

					LogText.SelectionFont = new Font (LogText.Font, FontStyle.Bold);
					LogText.SelectionColor = Color.DarkGreen;

					LogText.DeselectAll ();
					}

				if (LogText.Lines[i].Contains (MafiaGameForm.ActionSplitter))
					{
					/*start = start + LogText.Lines[i].IndexOf (MafiaGameForm.ActionSplitter);*/
					start += LogText.Lines[i].IndexOf (MafiaGameForm.ActionSplitter);
					end = LogText.Text.IndexOf (MafiaGameForm.ActionSplitter,
						start + MafiaGameForm.ActionSplitter.Length);
					LogText.Select (start, end + MafiaGameForm.ActionSplitter.Length - start);

					LogText.SelectionColor = Color.DarkRed;

					LogText.DeselectAll ();
					}
				}

			// Запуск
			this.ShowDialog ();
			}

		// Обновление позиции курсора
		private void MafiaLogForm_Shown (object sender, EventArgs e)
			{
			LogText.Refresh ();
			LogText.ScrollToCaret ();
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MafiaLogForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			// Сохранение настроек
			RDGenerics.SaveWindowDimensions (this);
			}

		// Изменение размеров окна
		private void MafiaLogForm_Resize (object sender, EventArgs e)
			{
			LogText.Width = this.Width - 41;
			LogText.Height = this.Height - 92;

			BExit.Top = this.Height - 74;
			BExit.Left = (this.Width - BExit.Width) / 2;
			}
		}
	}
