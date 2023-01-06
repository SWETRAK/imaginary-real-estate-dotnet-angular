import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {EmptyOfferResult, IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {EmptyImageOfferResultDto, IImageOfferResultDto} from "../../interfaces/IImageOfferResultDto";
import {OfferHttpService} from "../../services/offer-http.service";
import {LocalStorageService} from "../../services/local-storage.service";

@Component({
  selector: 'app-ire-offer-baner',
  templateUrl: './ire-offer-baner.component.html',
  styleUrls: ['./ire-offer-baner.component.css']
})
export class IreOfferBanerComponent implements OnInit {

    @Input("offer") offer: IOfferResultDto = EmptyOfferResult();
    @Input("liked") isLiked: boolean = false;

    @Output("likeChange") likeChange = new EventEmitter<boolean>();
    protected frontImage: IImageOfferResultDto = EmptyImageOfferResultDto();
    protected loggedIn: boolean = false;

    constructor(
        private offerHttpService: OfferHttpService,
        private localStorageService: LocalStorageService
    ) {

    }

    ngOnInit() {
        let frontImage: IImageOfferResultDto | undefined = this.offer.images.find(element => element.frontPhoto);
        const user = this.localStorageService.getUser()
        if(user !== null){
            this.loggedIn = true;
        }
        if (frontImage !== undefined) {
            this.frontImage = frontImage;
        } else {
            this.frontImage = EmptyImageOfferResultDto();
        }
    }

    protected addLike = () => {
        this.offerHttpService.likeOffer(this.offer.identifier).subscribe({
            next: (result: boolean) => {
                if (result) this.isLiked = true;
                this.likeChange.emit(this.isLiked);
            },
            error: (error: any) => {
                console.log(error);
            }
        });
    }

    protected removeLike = () => {
        this.offerHttpService.unlikeOffer(this.offer.identifier).subscribe({
            next: (result: boolean) => {
                if (result) this.isLiked = false;
                this.likeChange.emit(this.isLiked);
            },
            error: (error: any) => {
                console.log(error);
            }
        });
    }
}
