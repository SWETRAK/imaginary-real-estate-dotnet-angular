import {Component, OnInit} from '@angular/core';
import {IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {OfferHttpService} from "../../services/offer-http.service";
import {LocalStorageService} from "../../services/local-storage.service";
import {UserHttpService} from "../../services/user-http.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Route} from "@angular/router";

@Component({
  selector: 'app-ire-offers-page',
  templateUrl: './ire-offers-page.component.html',
  styleUrls: ['./ire-offers-page.component.css']
})
export class IreOffersPageComponent implements OnInit{

    private searchPhase: string | null = null;
    protected offersList: Array<IOfferResultDto> = new Array<IOfferResultDto>();
    private likedOffers: Array<IOfferResultDto> = new Array<IOfferResultDto>();
    protected searchForm: FormGroup;

    constructor(
        private offerHttpService: OfferHttpService,
        private localStorageService: LocalStorageService,
        private userHttpService: UserHttpService,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
    ) {
        this.searchForm = formBuilder.group({
            search: new FormControl('', [
                Validators.minLength(1)
            ])
        });

    }

    async ngOnInit() {
        this.searchPhase = this.route.snapshot.paramMap.get('searchPhase');
        if (this.searchPhase !== null) {
            this.getSearch(this.searchPhase);
        } else {
            this.getAllOffers();
        }
        const user = this.localStorageService.getUser();
        if(user != null) {
            this.getLikedOffers();
        }
    }

    protected search = () => {
        if(this.searchForm.valid){
            this.getSearch(this.searchForm.get("search")?.value);
        }
    }

    protected checkIfLiked = (offerId: String) => {
        if (this.likedOffers.length === 0) return false;
        return this.likedOffers.some(el => el.identifier === offerId);
    }

    protected statusChanged = () => {
        this.getLikedOffers();
        const user = this.localStorageService.getUser();
        if(user != null) {
            this.getLikedOffers();
        }
    }

    private getSearch = (search: string) => {
        this.offerHttpService.getOfferByAddress(search).subscribe({
            next: (response: Array<IOfferResultDto>) => {
                this.offersList = response;
            },
            error: (error: any) => {
                console.log(error);
            },
        })
    }

    private getAllOffers = () => {
        this.offerHttpService.getAllOffers().subscribe({
            next: (result: Array<IOfferResultDto>) =>{
                this.offersList = result;
            },
            error: (error: any) => {
                console.log(error);
            }
        });
    }

    private getLikedOffers = () => {
        this.userHttpService.getLikedOffers().subscribe({
            next: (result: Array<IOfferResultDto>) => {
                this.likedOffers = result;
            },
            error: (error: any) => {
                console.log(error);
            },
        });
    }

}
