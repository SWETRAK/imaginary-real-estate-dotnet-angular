import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreOfferFrontImageComponent } from './ire-offer-front-image.component';

describe('IreOfferFrontImageComponent', () => {
  let component: IreOfferFrontImageComponent;
  let fixture: ComponentFixture<IreOfferFrontImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreOfferFrontImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreOfferFrontImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
