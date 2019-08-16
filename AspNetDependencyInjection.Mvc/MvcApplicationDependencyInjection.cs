﻿using System;
using System.Web.Mvc;

using AspNetDependencyInjection.Internal;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetDependencyInjection
{
	public sealed class MvcApplicationDependencyInjection : ApplicationDependencyInjection
	{
		private const String _configureError = "Use " + nameof(ConfigureMvc) + " to set up dependency-injection for ASP.NET MVC.";

		[System.ComponentModel.EditorBrowsable( state: System.ComponentModel.EditorBrowsableState.Never )]
		[System.ComponentModel.Browsable( browsable: false )]
		[Obsolete( _configureError )]
		public new static ApplicationDependencyInjection Configure( Action<IServiceCollection> configureServices )
		{
			throw new NotSupportedException( _configureError );
		}

		[System.ComponentModel.EditorBrowsable( state: System.ComponentModel.EditorBrowsableState.Never )]
		[System.ComponentModel.Browsable( browsable: false )]
		[Obsolete( _configureError )]
		public new static ApplicationDependencyInjection Configure( ApplicationDependencyInjectionConfiguration configuration, Action<IServiceCollection> configureServices )
		{
			throw new NotSupportedException( _configureError );
		}

		//

		/// <summary>Call this method from a <see cref="WebActivatorEx.PreApplicationStartMethodAttribute"/>-marked method to instantiate a new <see cref="ApplicationDependencyInjection"/> and to configure services in the root service provider.</summary>
		public static MvcApplicationDependencyInjection ConfigureMvc( Action<IServiceCollection> configureServices )
		{
			return ConfigureMvc( new ApplicationDependencyInjectionConfiguration(), configureServices );
		}

		/// <summary>Call this method from a <see cref="WebActivatorEx.PreApplicationStartMethodAttribute"/>-marked method to instantiate a new <see cref="ApplicationDependencyInjection"/> and to configure services in the root service provider.</summary>
		public static MvcApplicationDependencyInjection ConfigureMvc( ApplicationDependencyInjectionConfiguration configuration, Action<IServiceCollection> configureServices )
		{
			if( configuration     == null ) throw new ArgumentNullException(nameof(configuration));
			if( configureServices == null ) throw new ArgumentNullException(nameof(configureServices));

			ServiceCollection services = new ServiceCollection();
			configureServices( services );

			return new MvcApplicationDependencyInjection( configuration, services );
		}

		private readonly IDependencyResolver                                     originalIdr;
		private readonly DependencyInjectionWebObjectActivatorDependencyResolver idr;

		private MvcApplicationDependencyInjection(ApplicationDependencyInjectionConfiguration configuration, IServiceCollection services)
			: base( configuration, services )
		{
			this.originalIdr = DependencyResolver.Current; // will never be null.
			this.idr         = new DependencyInjectionWebObjectActivatorDependencyResolver( this.WebObjectActivator );

			DependencyResolver.SetResolver( this.idr );
		}

		protected override void Dispose( Boolean disposing )
		{
			if( !base.IsDisposed )
			{
				// Restore original IDR:
				DependencyResolver.SetResolver( this.originalIdr );
			}

			base.Dispose( disposing );
		}
	}
}
