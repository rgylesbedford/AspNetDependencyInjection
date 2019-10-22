﻿using System;

using AspNetDependencyInjection.Internal;

namespace AspNetDependencyInjection
{
	public static class ApplicationDependencyInjectionBuilderExtensions
	{
		public static ApplicationDependencyInjectionBuilder AddWebApiDependencyResolver( this ApplicationDependencyInjectionBuilder builder )
		{
			if( builder == null ) throw new ArgumentNullException(nameof(builder));

			return builder
				.AddClient( ( di, rootSP ) => new DependencyInjectionWebApiDependencyResolver( di, rootSP ) );
		}
	}
}
