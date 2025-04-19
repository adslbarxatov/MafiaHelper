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
		/// Успешно; детектив должен стать шерифом
		/// </summary>
		SuccessTurnIntoSheriff = 3,

		/// <summary>
		/// Успешно; детектив должен стать шерифом (вариант для камикадзе)
		/// </summary>
		SuccessTurnIntoSheriffSelfToo = 4,

		/// <summary>
		/// Запрос пропущен, поскольку является заглушкой
		/// </summary>
		Skipped = 5,

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

	/// <summary>
	/// Возможные роли игрока
	/// </summary>
	public enum MafiaPlayerRoles
		{
		/// <summary>
		/// Обыватель, житель
		/// </summary>
		Townspeople,

		/// <summary>
		/// Мафия
		/// </summary>
		Mafia,

		/// <summary>
		/// Доктор, врач
		/// </summary>
		Doctor,

		/// <summary>
		/// Детектив, комиссар
		/// </summary>
		Detective,

		/// <summary>
		/// Красавица, путана
		/// </summary>
		Beauty,

		/// <summary>
		/// Маньяк
		/// </summary>
		Maniac,

		/// <summary>
		/// Бессмертный, священник
		/// </summary>
		Immortal,

		/// <summary>
		/// Вор
		/// </summary>
		Thief,

		/// <summary>
		/// Камикадзе
		/// </summary>
		Kamikaze,

		/// <summary>
		/// Босс мафии
		/// </summary>
		MafiaBoss,

		/// <summary>
		/// Шериф / комиссар
		/// </summary>
		Sheriff,

		/// <summary>
		/// Потрошитель
		/// </summary>
		Ripper,

		/// <summary>
		/// Судья
		/// </summary>
		Jugde,

		/// <summary>
		/// Якудза
		/// </summary>
		Yakuza,

		/// <summary>
		/// Босс якудза
		/// </summary>
		YakuzaBoss,
		}
	}
