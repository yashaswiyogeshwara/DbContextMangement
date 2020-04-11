using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public class DbContextScope : IDisposable
    {
        private static int? currentContextId { get; set; }
        private static Dictionary<int?, DbContextScope> DbConextScopeCollection = new Dictionary<int?, DbContextScope>();
        private Dictionary<Type, IContext> _contextsDictionary;            
        private int? _instanceId = new Random().Next(10000);
        private bool _nested;
        private DbContextScope _parentScope;

        public DbContextScope() {
            DbConextScopeCollection.Add(_instanceId, this);
            if (currentContextId != null) {
                _nested = true;
                
                 if(DbConextScopeCollection.TryGetValue(currentContextId, out _parentScope)){
                    _contextsDictionary = _parentScope._contextsDictionary;
                };
                currentContextId = _instanceId;
                return;
            }
            _contextsDictionary = new Dictionary<Type, IContext>();
            currentContextId = _instanceId;

        }

        public TContext Get<TContext>() where TContext : DbContext{
            Type t = typeof(TContext);
            IContext context;
            if (!_contextsDictionary.TryGetValue(t, out context)) {
                DbContext newContext = Activator.CreateInstance<TContext>();
                newContext.Database.BeginTransaction();
                context = newContext as IContext;
                _contextsDictionary.Add(t, context);
            }
            return context as TContext;
        } 
        public void SaveChanges() {
            if (!_nested) {
                foreach(IContext context in _contextsDictionary.Values)
                {
                    context.SaveChanges();
                }
            }
        }

        public void Dispose()
        {
            if (!_nested) {
                foreach (IContext context in _contextsDictionary.Values) {
                    context.Commit(); // rollback
                }
                currentContextId = null;
                _nested = false;
                return;
            }
            currentContextId = this._parentScope._instanceId;
        }
    }
}
