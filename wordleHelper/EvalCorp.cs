﻿namespace wordleHelper
{
    public class EvalCorp
    {
        private readonly List<char> _availables = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray().ToList();
        private readonly Field _field = new();

        public List<string> Evaluate()
        {
            var list = Wordlist.CreateInstance();

            // Apply all sorts of filters here.

            // available chars 
            var unavailableChars = _field.GetUnavailableChars();
            var availableChars = _availables.Where(c => !unavailableChars.Contains(c));
            bool PredAvailChars(string c) => c.ToCharArray().All(value => availableChars.Contains(value));
            
            // mustcontains
            var mustcontains = _field.GetMustContains();
            bool PredMustContainChars(string word) => mustcontains.Length == 0 || mustcontains.All(must => word.ToCharArray().Contains(must));

            // must pos
            var mustPos = _field.GetMustPos();
            bool PredMustPos(string word)
            {
                if (mustPos.Count == 0) return true;

                var ret = true;
                foreach (var (pos, buchstabe) in mustPos)
                {
                    ret &= word.Substring(pos, 1) == buchstabe.ToString();
                }

                return ret;
            }

            // cannot pos
            var cannotPos = _field.GetCannotPos();
            bool PredCannotPos(string word)
            {
                if (cannotPos.Count == 0) return true;

                var ret = true;
                foreach (var (pos, buchstabe) in cannotPos)
                {
                    ret &= word.Substring(pos, 1) != buchstabe.ToString();
                }

                return ret;
            }

            return list.AsParallel().AsOrdered()
                .Where(PredAvailChars)
                .Where(PredMustContainChars)
                .Where(PredMustPos)
                .Where(PredCannotPos)
                .ToList();
        }

        public void SetConditionGreen(byte line, byte row)
        {
            _field.SetCondition(FieldColor.Green, line, row);
        }

        public void SetConditionYellow(byte line, byte row)
        {
            _field.SetCondition(FieldColor.Yellow, line, row);
        }

        public void SetConditionGray(byte line, byte row)
        {
            _field.SetCondition(FieldColor.Gray, line, row);
        }

        public void UnSetCondition(byte line, byte row)
        {
            _field.SetCondition(FieldColor.Unset, line, row);
        }

        public void SetChar(char? c, byte line, byte row)
        {
            _field.SetChar(c, line, row);
        }
    }
}