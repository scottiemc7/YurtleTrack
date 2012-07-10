using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Presenter
{
	interface IOptionsPresenter
	{
		void Initalize();
		void Rollback();
		void Save();
	}
}
