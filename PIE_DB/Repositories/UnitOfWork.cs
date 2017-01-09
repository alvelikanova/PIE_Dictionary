using System;
using PIE_DB.Data_Model;

namespace PIE_DB.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private PIE_Entities context = new PIE_Entities();

        private GenericRepository<Branch> branchRepository;
        private GenericRepository<Dictionary_conformity> dictionaryConformityRepository;
        private GenericRepository<Language> languageRepository;
        private GenericRepository<Phoneme> phonemeRepository;
        private GenericRepository<Phonetic_matching> phoneticMatchingRepository;
        private GenericRepository<Rule> ruleRepository;
        private GenericRepository<Vocabulary_entry> vocabularyEntryRepository;

        public GenericRepository<Branch> BranchRepository
        {
            get
            {
                if (this.branchRepository == null)
                {
                    this.branchRepository = new GenericRepository<Branch>(context);
                }
                return branchRepository;
            }
        }

        public GenericRepository<Dictionary_conformity> DictionaryConformityRepository
        {
            get
            {
                if (this.dictionaryConformityRepository == null)
                {
                    this.dictionaryConformityRepository = new GenericRepository<Dictionary_conformity>(context);
                }
                return dictionaryConformityRepository;
            }
        }

        public GenericRepository<Language> LanguageRepository
        {
            get
            {
                if (this.languageRepository == null)
                {
                    this.languageRepository = new GenericRepository<Language>(context);
                }
                return languageRepository;
            }
        }

        public GenericRepository<Phoneme> PhonemeRepository
        {
            get
            {
                if (this.phonemeRepository == null)
                {
                    this.phonemeRepository = new GenericRepository<Phoneme>(context);
                }
                return phonemeRepository;
            }
        }

        public GenericRepository<Phonetic_matching> PhoneticMatchingRepository
        {
            get
            {
                if (this.phoneticMatchingRepository == null)
                {
                    this.phoneticMatchingRepository = new GenericRepository<Phonetic_matching>(context);
                }
                return phoneticMatchingRepository;
            }
        }

        public GenericRepository<Rule> RuleRepository
        {
            get
            {
                if (this.ruleRepository == null)
                {
                    this.ruleRepository = new GenericRepository<Rule>(context);
                }
                return ruleRepository;
            }
        }
        public GenericRepository<Vocabulary_entry> VocabularyEntryRepository
        {
            get
            {
                if (this.vocabularyEntryRepository == null)
                {
                    this.vocabularyEntryRepository = new GenericRepository<Vocabulary_entry>(context);
                }
                return vocabularyEntryRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
