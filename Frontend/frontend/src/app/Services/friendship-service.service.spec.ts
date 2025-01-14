import { TestBed } from '@angular/core/testing';

import { FriendshipService } from '../Services/friendship-service.service';

describe('FriendshipServiceService', () => {
  let service: FriendshipService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FriendshipService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
