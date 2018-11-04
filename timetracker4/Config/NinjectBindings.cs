﻿using Ninject.Modules;
using timetracker4.Services;

namespace timetracker4.Config
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<Database>().To<Database>().InSingletonScope();
        }
    }
}