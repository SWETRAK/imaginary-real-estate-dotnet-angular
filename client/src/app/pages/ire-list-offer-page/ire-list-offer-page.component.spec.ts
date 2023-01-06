import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreListOfferPageComponent } from './ire-list-offer-page.component';

describe('IreListOfferPageComponent', () => {
  let component: IreListOfferPageComponent;
  let fixture: ComponentFixture<IreListOfferPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreListOfferPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreListOfferPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
