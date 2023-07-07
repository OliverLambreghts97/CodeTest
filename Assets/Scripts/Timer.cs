using UnityEngine;

namespace TankGame
{
	public struct Timer
	{
		private double? _targetTime;

		public bool IsRunning => IsValid() && Time.timeAsDouble < _targetTime;

		public static Timer None => new Timer();

		public static Timer CreateTimer(float duration)
		{
			return new Timer { _targetTime = Time.timeAsDouble + duration };
		}

		public void RestartTimer(float duration)
		{
			_targetTime = Time.timeAsDouble + duration;
		}

		public bool IsValid()
		{
			return _targetTime > 0;
		}

		public void Invalidate()
		{
			_targetTime = -1;
		}

		public double? RemainingTime()
		{
			return IsRunning ? _targetTime - Time.timeAsDouble : null;
		}

		public bool Expired()
		{
			return IsValid() && Time.timeAsDouble >= _targetTime;
		}

		public bool ExpiredOrNotRunning()
		{
			return IsValid() == false || Expired();
		}

		public override string ToString()
		{
			return $"{Time.timeAsDouble}/{_targetTime}";
		}
	}
}