import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule} from "@angular/router";
import { AppComponent } from './app.component';
import { IreTopBarComponent } from './components/ire-topbar/ire-topbar.component';
import { HttpClientModule } from "@angular/common/http";
import { IreFooterComponent } from './components/ire-footer/ire-footer.component';
import { routes } from "./Routes";
import { IreLoginPageComponent } from './pages/ire-login-page/ire-login-page.component';
import { IreRegisterPageComponent } from './pages/ire-register-page/ire-register-page.component';
import { IreHomePageComponent } from './pages/ire-home-page/ire-home-page.component';
import { IreOffersPageComponent } from './pages/ire-offers-page/ire-offers-page.component';
import { IreFavouritesPageComponent } from './pages/ire-favourites-page/ire-favourites-page.component';
import { IreListOfferPageComponent } from './pages/ire-list-offer-page/ire-list-offer-page.component';
import { IreNotFoundPageComponent } from './pages/ire-not-found-page/ire-not-found-page.component';
import { IreServerErrorPageComponent } from './pages/ire-server-error-page/ire-server-error-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from "@angular/forms";
import { IreOfferBanerComponent } from './components/ire-offer-baner/ire-offer-baner.component';
import { IreOfferFrontImageComponent } from './components/ire-offer-front-image/ire-offer-front-image.component';
import { IreOfferInfoPageComponent } from './pages/ire-offer-info-page/ire-offer-info-page.component';
import { IreListOfferModalComponent } from './components/ire-list-offer-modal/ire-list-offer-modal.component';
import { IreOfferAuthorBanerComponent } from './components/ire-offer-author-baner/ire-offer-author-baner.component';
import { IreLogoutPageComponent } from './pages/ire-logout-page/ire-logout-page.component';
import { IreContactPageComponent } from './pages/ire-contact-page/ire-contact-page.component';
import { IreUserInfoPageComponent } from './pages/ire-user-info-page/ire-user-info-page.component';
import { IreIsLoggedInGuard } from "./guards/ire-is-logged-in.guard";
import { IreIsAuthorGuard } from "./guards/ire-is-author.guard";
import { IreIsLoggedOutGuard } from "./guards/ire-is-logged-out.guard";
import { IreChangePasswordComponent } from './pages/ire-change-password/ire-change-password.component';

@NgModule({
    declarations: [
        AppComponent,
        IreTopBarComponent,
        IreFooterComponent,
        IreLoginPageComponent,
        IreRegisterPageComponent,
        IreHomePageComponent,
        IreOffersPageComponent,
        IreFavouritesPageComponent,
        IreListOfferPageComponent,
        IreNotFoundPageComponent,
        IreServerErrorPageComponent,
        IreOfferBanerComponent,
        IreOfferFrontImageComponent,
        IreOfferInfoPageComponent,
        IreListOfferModalComponent,
        IreOfferAuthorBanerComponent,
        IreLogoutPageComponent,
        IreContactPageComponent,
        IreUserInfoPageComponent,
        IreChangePasswordComponent,
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
        HttpClientModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
    ],
    providers: [
        IreIsLoggedInGuard,
        IreIsAuthorGuard,
        IreIsLoggedOutGuard,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
