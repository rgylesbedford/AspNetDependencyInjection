﻿using System;

using AspNetDependencyInjection.Internal;

namespace AspNetDependencyInjection
{
	/// <summary>Extension methods for <see cref="ApplicationDependencyInjectionBuilder"/>.</summary>
	public static class ApplicationDependencyInjectionBuilderExtensions
	{
		/// <summary>Call this method to set-up AspNetDependencyInjection for ASP.NET Web API.</summary>
		/// <param name="builder">Cannot be null.</param>
		/// <remarks>Internally, this method adds <see cref="DependencyInjectionWebApiDependencyResolver"/> to <paramref name="builder"/> using <see cref="ApplicationDependencyInjectionBuilder.AddClient(Func{ApplicationDependencyInjection, IDependencyInjectionClient}[])"/>.</remarks>
		public static ApplicationDependencyInjectionBuilder AddWebApiDependencyResolver( this ApplicationDependencyInjectionBuilder builder )
		{
			if( builder == null ) throw new ArgumentNullException(nameof(builder));

			return builder
				.AddClient( ( di, rootSP ) => new DependencyInjectionWebApiDependencyResolver( di, rootSP ) );
		}
	}
}
