using System;
using System.Diagnostics.Contracts;

namespace BoundTree.Logic
{
    [Serializable]
    public class LogicLevel : IEquatable<LogicLevel>
    {
        private readonly int? _level;

        public LogicLevel() { }

        public LogicLevel(int level)
        {
            Contract.Requires(level >= 0);

            _level = level;
        }

        public static bool operator ==(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            var first = firstLevel as object;
            var second = secondLevel as object;
            if (first == null && second == null)
            {
                return true;
            }

            if (first == null)
            {
                return false;
            }

            return firstLevel.Equals(secondLevel);
        }

        public static bool operator !=(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            return !(firstLevel == secondLevel);
        }

        public static bool operator >(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            Validate(firstLevel, secondLevel);

            return firstLevel._level.Value > secondLevel._level.Value;
        }

        public static bool operator >=(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            Validate(firstLevel, secondLevel);

            return firstLevel == secondLevel || firstLevel > secondLevel;
        }

        public static bool operator <=(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            Validate(firstLevel, secondLevel);

            return firstLevel == secondLevel || firstLevel < secondLevel;
        }

        public static bool operator <(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            Validate(firstLevel, secondLevel);

            return firstLevel._level.Value < secondLevel._level.Value;
        }

        public bool Equals(LogicLevel otherLevel)
        {
            var other = otherLevel as Object;
            if (other == null)
            {
                return false;
            }

            return _level.Value.Equals(otherLevel._level.Value);
        }

        private static void Validate(LogicLevel firstLevel, LogicLevel secondLevel)
        {
            var first = firstLevel as Object;
            var second = secondLevel as Object;

            if (first == null || second == null)
            {
                throw new ArgumentNullException("Level");
            }

            if (!firstLevel._level.HasValue || !secondLevel._level.HasValue)
            {
                throw new InvalidOperationException("_level == null");
            }

        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LogicLevel)obj);
        }

        public override int GetHashCode()
        {
            return _level.GetHashCode();
        }
    }
}