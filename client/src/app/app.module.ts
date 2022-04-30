import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_AUTOCOMPLETE_SCROLL_STRATEGY_FACTORY_PROVIDER } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MsalModule, MsalService, MSAL_INSTANCE } from '@azure/msal-angular';
import {
  IPublicClientApplication,
  PublicClientApplication
} from '@azure/msal-browser';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AvatarModule } from 'ngx-avatar';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateWorkFlowModule } from './create-workflow/create-workflow.module';
import { LoginModule } from './login/login.module';
import { NavbarModule } from './navbar/navbar.module';
import { WorkFlowsModule } from './notifications/workflows.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

export function HttpLoaderFacotry(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: '8c898c70-5cb4-4277-b7f0-d16fa0e2a910',
      redirectUri: 'https://luupadmin.azurewebsites.net/',
    },
  });
}

@NgModule({
  declarations: [AppComponent, PageNotFoundComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MsalModule,
    NavbarModule,
    TranslateModule.forRoot({
      defaultLanguage: 'en-US',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFacotry,
        deps: [HttpClient],
      },
    }),
    MatIconModule,
    LoginModule,
    WorkFlowsModule,
    CreateWorkFlowModule,
    MatFormFieldModule,
    MatInputModule,
    AvatarModule,
    LoginModule,
  ],
  providers: [
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    MsalService,
    MAT_AUTOCOMPLETE_SCROLL_STRATEGY_FACTORY_PROVIDER,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
