import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {INewOfferIncomingDto} from "../interfaces/INewOfferIncomingDto";
import {IOfferResultDto} from "../interfaces/IOfferResultDto";

@Injectable({
  providedIn: 'root'
})
export class OfferHttpService {

    constructor(private http: HttpClient) {
    }

    private urlBase: string = "https://localhost:7201/offers/";

    public getAllOffers() {
        return this.http.get<Array<IOfferResultDto>>(this.urlBase);
    }

    public getOfferByAddress(address: string) {
        return this.http.get<Array<IOfferResultDto>>(this.urlBase + address);
    }

    public createOffer(newOfferIncomingDto: INewOfferIncomingDto) {
        return this.http.post<IOfferResultDto>(this.urlBase, newOfferIncomingDto, {withCredentials: true});
    }

    public getOfferById(identifier: string) {
        return this.http.get<IOfferResultDto>(this.urlBase + "details/" + identifier);
    }

    public deleteOfferById(identifier: string) {
        return this.http.delete(this.urlBase + identifier, {withCredentials: true});
    }

    public likeOffer(offerIdentifier: string) {
        return this.http.post<boolean>(this.urlBase +  offerIdentifier + "/like", null, {withCredentials: true});
    }

    public unlikeOffer(offerIdentifier: string) {
        return this.http.post<boolean>(this.urlBase +  offerIdentifier + "/unlike", null, {withCredentials: true});
    }
}
