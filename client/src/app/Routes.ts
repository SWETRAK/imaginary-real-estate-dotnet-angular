import {Routes} from "@angular/router";
import {IreLoginPageComponent} from "./pages/ire-login-page/ire-login-page.component";
import {IreRegisterPageComponent} from "./pages/ire-register-page/ire-register-page.component";
import {IreHomePageComponent} from "./pages/ire-home-page/ire-home-page.component";
import {IreOffersPageComponent} from "./pages/ire-offers-page/ire-offers-page.component";
import {IreFavouritesPageComponent} from "./pages/ire-favourites-page/ire-favourites-page.component";
import {IreListOfferPageComponent} from "./pages/ire-list-offer-page/ire-list-offer-page.component";
import {IreNotFoundPageComponent} from "./pages/ire-not-found-page/ire-not-found-page.component";
import {IreServerErrorPageComponent} from "./pages/ire-server-error-page/ire-server-error-page.component";
import {IreOfferInfoPageComponent} from "./pages/ire-offer-info-page/ire-offer-info-page.component";
import {IreLogoutPageComponent} from "./pages/ire-logout-page/ire-logout-page.component";
import {IreContactPageComponent} from "./pages/ire-contact-page/ire-contact-page.component";
import {IreUserInfoPageComponent} from "./pages/ire-user-info-page/ire-user-info-page.component";
import {IreIsLoggedInGuard} from "./guards/ire-is-logged-in.guard";
import {IreIsLoggedOutGuard} from "./guards/ire-is-logged-out.guard";
import {IreIsAuthorGuard} from "./guards/ire-is-author.guard";
import {IreChangePasswordComponent} from "./pages/ire-change-password/ire-change-password.component";

export const routes: Routes = [
    { path: "", redirectTo: "/home", pathMatch: "full" },
    { path: "home", component: IreHomePageComponent },
    { path: "contact", component: IreContactPageComponent },
    { path: "auth/login", component: IreLoginPageComponent, canActivate: [ IreIsLoggedOutGuard ] },
    { path: "auth/register", component: IreRegisterPageComponent, canActivate: [ IreIsLoggedOutGuard ] },
    { path: "auth/logout", component: IreLogoutPageComponent, canActivate: [ IreIsLoggedInGuard ] },
    { path: "offers", component: IreOffersPageComponent },
    { path: "offers/:searchPhase", component: IreOffersPageComponent },
    { path: "favourites", component: IreFavouritesPageComponent, canActivate: [ IreIsLoggedInGuard ]},
    { path: "details/:identifier", component: IreOfferInfoPageComponent },
    { path: "user/change/password", component: IreChangePasswordComponent },
    { path: "user/info", component: IreUserInfoPageComponent, canActivate: [ IreIsLoggedInGuard ]},
    { path: "list", component: IreListOfferPageComponent, canActivate: [ IreIsAuthorGuard ] },
    { path: "500", component: IreServerErrorPageComponent },
    { path: "404", component: IreNotFoundPageComponent },
    { path: "**", redirectTo: "/404", pathMatch: "full" }
];
