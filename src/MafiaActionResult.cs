namespace RD_AAOW
	{
	/// <summary>
	/// Возможные результаты действий в игре
	/// </summary>
	public enum MafiaActionResult
		{
		/// <summary>
		/// Успешно
		/// </summary>
		Success = 0,

		/// <summary>
		/// Успешно; такое же действие нужно применить к зависимому игроку
		/// (красотка)
		/// </summary>
		SuccessRecursively = 1,

		/// <summary>
		/// Успешно; такое же действие нужно применить к самому себе
		/// (камикадзе)
		/// </summary>
		SuccessSelfToo = 2,

		/// <summary>
		/// Неуспешно: неприменимо для этой роли игрока
		/// </summary>
		FailIncorrectRole = -1,

		/// <summary>
		/// Неуспешно: неприменимо для этого состояния игрока
		/// </summary>
		FailIncorrectState = -2,

		/// <summary>
		/// Неуспешно: целевой игрок защищён правилами
		/// </summary>
		FailTargetProtected = -3,
		}
	}
