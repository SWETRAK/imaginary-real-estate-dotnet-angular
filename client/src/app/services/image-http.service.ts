import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IImageOfferResultDto} from "../interfaces/IImageOfferResultDto";

@Injectable({
  providedIn: 'root'
})
export class ImageHttpService {

    constructor(
        private http: HttpClient
    ) {
    }

    private urlBase: string = "http://localhost:8080/images/";

    public uploadImage(dane: FormData) {
        return this.http.post<IImageOfferResultDto>(this.urlBase, dane, {withCredentials: true});
    }
}
