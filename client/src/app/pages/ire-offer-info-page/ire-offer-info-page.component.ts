import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {OfferHttpService} from "../../services/offer-http.service";
import {EmptyOfferResult, IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {IImageOfferResultDto} from "../../interfaces/IImageOfferResultDto";

@Component({
  selector: 'app-ire-offer-info-page',
  templateUrl: './ire-offer-info-page.component.html',
  styleUrls: ['./ire-offer-info-page.component.css']
})
export class IreOfferInfoPageComponent implements OnInit {

    private identifier: string | null = null;
    protected offer: IOfferResultDto = EmptyOfferResult();
    protected frontImage: string = "/assets/img/pexelsLublin.jpg";

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private offerHttpService: OfferHttpService
    ) {
    }

    ngOnInit() {
        this.identifier = this.route.snapshot.paramMap.get('identifier');
        if(this.identifier) {
            this.offerHttpService.getOfferById(this.identifier).subscribe({
                next: (result: IOfferResultDto) => {
                    this.offer = result;
                    const frontImageObject = this.offer.images.find((image:IImageOfferResultDto) => image.frontPhoto);
                    if(frontImageObject) {
                        this.frontImage = "https://localhost:7201/images/"+ frontImageObject.identifier;
                    } else {
                        this.frontImage = "/assets/img/pexelsLublin.jpg";
                    }
                },
                error: (error: any) => {
                    console.log(error);
                }
            });
        }
    }

}
