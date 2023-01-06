import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {EmptyOfferResult, IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {OfferHttpService} from "../../services/offer-http.service";
import {EmptyImageOfferResultDto, IImageOfferResultDto} from "../../interfaces/IImageOfferResultDto";

@Component({
  selector: 'app-ire-offer-author-baner',
  templateUrl: './ire-offer-author-baner.component.html',
  styleUrls: ['./ire-offer-author-baner.component.css']
})
export class IreOfferAuthorBanerComponent implements OnInit{

    @Input("offer") offer: IOfferResultDto = EmptyOfferResult();

    @Output("removed") removed= new EventEmitter<string>();
    protected frontImage: IImageOfferResultDto = EmptyImageOfferResultDto();

    constructor(
        private offerHttpService: OfferHttpService
    ) {

    }

    ngOnInit() {
        let frontImage: IImageOfferResultDto | undefined = this.offer.images.find(element => element.frontPhoto);
        if (frontImage !== undefined) {
            this.frontImage = frontImage;
        } else {
            this.frontImage = EmptyImageOfferResultDto();
        }
    }

    protected removeOffer = () => {
        this.offerHttpService.deleteOfferById(this.offer.identifier).subscribe({
            next: (result: any) => {
                this.removed.emit(this.offer.identifier);
            },
            error: (error: any) => {
                console.log(error)
            }
        });
    }

}
