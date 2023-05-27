import {Component, Input, OnInit} from '@angular/core';
import {EmptyImageOfferResultDto, IImageOfferResultDto} from "../../interfaces/IImageOfferResultDto";

@Component({
  selector: 'app-ire-offer-front-image',
  templateUrl: './ire-offer-front-image.component.html',
  styleUrls: ['./ire-offer-front-image.component.css']
})
export class IreOfferFrontImageComponent implements OnInit{

    @Input("image") image: IImageOfferResultDto;
    protected photoUrl: string = 'http://localhost:8080/images/';

    constructor() {
        this.image = EmptyImageOfferResultDto();
    }

    ngOnInit() {
        if(this.image.frontPhoto) {
            this.photoUrl += this.image.identifier;
        } else {
            this.photoUrl = "/assets/img/pexelsKrakow.jpg"
        }
    }
}
