﻿using System.IO;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Common;
using Sitecore.Mvc.ExperienceEditor.Pipelines.RenderPageExtenders;

namespace SitecoreSidekick.Pipelines.ExperienceEditor
{
	class RegisterSidekickForEe : RenderPageExtendersProcessor
	{
		public override void Process(RenderPageExtendersArgs args)
		{
			Assert.ArgumentNotNull(args, "args");
			args.Disposables.Add(new GenericDisposable(() => args.Writer.Write(@"
<script src='/scs/scscommand.js'>

</script>")));
		}

		protected override bool Render(TextWriter writer)
		{
			return false;
		}
	}
}
