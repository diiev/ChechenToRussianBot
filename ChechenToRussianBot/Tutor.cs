using System;
using System.Collections.Generic;
using System.Text;

namespace ChechenToRussianBot
{
  public  class Tutor
    {
        private WordStorage _storage = new WordStorage();
        private   Dictionary<string, string> _dic;
        
        private Random _rand = new Random();
        
        public Tutor ()
        {
            _dic = _storage.GetAllWords();
        }
        public void AddWord (string chechen,  string rus)
        {  
            if (!_dic.ContainsKey(chechen))
            {
                _dic.Add(chechen, rus);
                _storage.AddWord(chechen, rus);
            }
           
        } 

        public  bool CheckWord (string chechen, string rus)
        {
            var answer = _dic[chechen];
            return answer.ToLower() == rus.ToLower();
        }
        public string Translate (string chechen)
        {
            return _dic.ContainsKey(chechen) ? _dic[chechen] : null;
        }  
        public string GetRandomChechWord ()
        {
                var r = _rand.Next(0, _dic.Count);
                var keys = new List<string>(_dic.Keys);
                return keys[r];
  
        }
    }
}
